using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.BLL.Shared;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Components
{
    public class MotorVehicleComponent : IMotorVehicleComponent
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;
        private readonly ILicensePlateRepository _licensePlateRepository;
        private readonly ILicensePlateSnapshotRepository _licensePlateSnaphotRepository;
        private readonly LicensePlateValidator _licensePlateValidator;
        private readonly MotorVehicleValidator _motorVehicleValidator;

        public MotorVehicleComponent(
            IMotorVehicleRepository motorVehicleRepository,
            ILicensePlateRepository licensePlateRepository,
            ILicensePlateSnapshotRepository licensePlateSnaphotRepository,
            LicensePlateValidator licensePlateValidator,
            MotorVehicleValidator motorVehicleValidator)
        {
            _motorVehicleRepository = motorVehicleRepository;
            _licensePlateRepository = licensePlateRepository;
            _licensePlateSnaphotRepository = licensePlateSnaphotRepository;
            _licensePlateValidator = licensePlateValidator;
            _motorVehicleValidator = motorVehicleValidator;
        }

        public async Task<IComponentResponse> AssignLicensePlateToMotorVehicleAsync(IAssignLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(contract.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return ComponentResponse.BadRequest("Could not find the license plate with given id.");
            if (licensePlate.InUse)
                return ComponentResponse.BadRequest("The given license plate is already in use. Please detach the license plate before assigning.");

            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdAsync(licensePlate.Id, cancellationToken);
            if (motorVehicle is not null)
                if (motorVehicle.Id == contract.MotorVehicleId)
                    return ComponentResponse.NoContent();
                else
                    return ComponentResponse.BadRequest($"This license plate already has been assigned. Please withdraw the license plate from the vehicle {motorVehicle.Brand}, {motorVehicle.Model} first.");

            motorVehicle = await _motorVehicleRepository.FindByIdAsync(contract.MotorVehicleId, cancellationToken);
            if (motorVehicle is null)
                return ComponentResponse.BadRequest("Could not find the motor vehicle with given id.");


            motorVehicle.LicensePlates.Add(licensePlate);


            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving the database.");


            return ComponentResponse.Ok();
        }

        public async Task<IComponentResponse> ChangeLicensePlateInUseStatusAsync(IChangeLicensePlateInUseStatusContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(contract.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return ComponentResponse.BadRequest("Could not find the license plate with given id.");

            if (licensePlate.InUse == contract.InUse)
                return ComponentResponse.NoContent();

            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdAsync(licensePlate.Id, cancellationToken);
            if (motorVehicle is null)
                return ComponentResponse.BadRequest("You can not put a plate in use, that is not assigned to a car.");

            var anyLicensePlateInUse = motorVehicle.LicensePlates
                                        .Where(licensePlate => licensePlate.InUse)
                                        .Any();

            if (anyLicensePlateInUse is true && contract.InUse is true)
                return ComponentResponse.BadRequest("There is already a plate in use on this motor vehicle. Please detach that plate first.");

            var snapshot = new LicensePlateSnapshot 
            { 
                InUse = contract.InUse,
                LicensePlate = licensePlate,
                MotorVehicle = motorVehicle,
                SnapshotDate = DateTime.Now 
            };

             _licensePlateSnaphotRepository.Add(snapshot);
            licensePlate.InUse = contract.InUse;

            var saved = await _licensePlateRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving the database.");


            return ComponentResponse.Ok();
        }

        public async Task<IComponentResponse> ChangeMotorVehicleOperationalStatusAsync(IChangeMotorVehicleOperationalStatusContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByIdAsync(contract.MotorVehicleId, cancellationToken);
            if (motorVehicle is null)
                return ComponentResponse.BadRequest("Could not find a motor vehicle with given id.");

            motorVehicle.Operational = contract.Operational;

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Ok();
        }

        public async Task<IComponentResponse> CreateLicensePlateAsync(ICreateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);
            if (licensePlate is not null)
                return ComponentResponse.BadRequest("The license plate with given identifier already exists.");

            licensePlate = new LicensePlate { Identifier = contract.Identifier };

            var validation = await _licensePlateValidator.ValidateAsync(licensePlate, cancellationToken);
            if (validation.IsValid is not true)
                return ComponentResponse.BadRequest(validation);

            _licensePlateRepository.Add(licensePlate);

            var saved = await _licensePlateRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Created();
        }

        public async Task<IComponentResponse> CreateMotorVehicleAsync(ICreateMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumber(contract.ChassisNumber, cancellationToken);
            if (motorVehicle is not null) 
                return ComponentResponse.BadRequest("The given vehicle already exists");

            motorVehicle = new MotorVehicle
            {
                 PropulsionType = (MotorVehiclePropulsionType) contract.PropulsionType,
                 BodyType = (MotorVehicleBodyType) contract.BodyType,
                 Brand = contract.Brand,
                 Model = contract.Model,
                 ChassisNumber = contract.ChassisNumber,
                 Operational = contract.Operational
            };

            var validation = await _motorVehicleValidator.ValidateAsync(motorVehicle, cancellationToken);
            if (validation.IsValid is not true)
                return ComponentResponse.BadRequest(validation);

            _motorVehicleRepository.Add(motorVehicle);

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Created();
        }

        public async Task<IComponentResponse> DeleteLicensePlateAsync(IDeleteLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(contract.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return ComponentResponse.BadRequest("Could not find the license plate with given id.");
            if (licensePlate.InUse)
                return ComponentResponse.BadRequest("The given license plate is in use. Please deactive the license plate before removing.");

            _licensePlateRepository.Remove(licensePlate);

            var saved = await _licensePlateRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Ok();
        }

        public async Task<IComponentResponse> WithdrawLicensePlateFromMotorVehicleAsync(IWithdrawLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(contract.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return ComponentResponse.BadRequest("Could not find the license plate with given id.");
            if (licensePlate.InUse)
                return ComponentResponse.BadRequest("The given license plate is in use. Please deactive the license plate before removing.");

            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdAsync(licensePlate.Id, cancellationToken);
            if (motorVehicle is null)
                return ComponentResponse.NoContent();

            motorVehicle.LicensePlates.Remove(licensePlate);

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Ok();
        }
    }
}
