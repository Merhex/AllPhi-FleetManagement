using FleetManagement.BLL.MotorVehicles.Commands;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.BLL.Shared;
using FleetManagement.BLL.Shared.Interfaces;
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

        public async Task<ICommandResponse> AssignLicensePlateToMotorVehicleAsync(AssignLicensePlateCommand command, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(command.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return CommandResponse.BadRequest("Could not find the license plate with given id.");
            if (licensePlate.InUse)
                return CommandResponse.BadRequest("The given license plate is already in use. Please detach the license plate before assigning.");

            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdAsync(licensePlate.Id, cancellationToken);
            if (motorVehicle is not null)
                if (motorVehicle.Id == command.MotorVehicleId)
                    return CommandResponse.NoContent();
                else
                    return CommandResponse.BadRequest($"This license plate already has been assigned. Please withdraw the license plate from the vehicle {motorVehicle.Brand}, {motorVehicle.Model} first.");

            motorVehicle = await _motorVehicleRepository.FindByIdAsync(command.MotorVehicleId, cancellationToken);
            if (motorVehicle is null)
                return CommandResponse.BadRequest("Could not find the motor vehicle with given id.");


            motorVehicle.LicensePlates.Add(licensePlate);


            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving the database.");


            return CommandResponse.Ok();
        }

        public async Task<ICommandResponse> ChangeLicensePlateInUseStatusAsync(ChangeLicensePlateInUseStatusCommand command, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(command.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return CommandResponse.BadRequest("Could not find the license plate with given id.");

            if (licensePlate.InUse == command.InUse)
                return CommandResponse.NoContent();

            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdAsync(licensePlate.Id, cancellationToken);
            if (motorVehicle is null)
                return CommandResponse.BadRequest("You can not put a plate in use, that is not assigned to a car.");

            var anyLicensePlateInUse = motorVehicle.LicensePlates
                                        .Where(licensePlate => licensePlate.InUse)
                                        .Any();

            if (anyLicensePlateInUse is true && command.InUse is true)
                return CommandResponse.BadRequest("There is already a plate in use on this motor vehicle. Please detach that plate first.");

            var snapshot = new LicensePlateSnapshot 
            { 
                InUse = command.InUse,
                LicensePlate = licensePlate,
                MotorVehicle = motorVehicle,
                SnapshotDate = DateTime.Now 
            };

             _licensePlateSnaphotRepository.Add(snapshot);
            licensePlate.InUse = command.InUse;

            var saved = await _licensePlateRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving the database.");


            return CommandResponse.Ok();
        }

        public async Task<ICommandResponse> ChangeMotorVehicleOperationalStatusAsync(ChangeMotorVehicleOperationalStatusCommand command, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByIdAsync(command.MotorVehicleId, cancellationToken);
            if (motorVehicle is null)
                return CommandResponse.BadRequest("Could not find a motor vehicle with given id.");

            motorVehicle.Operational = command.Operational;

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }

        public async Task<ICommandResponse> CreateLicensePlateAsync(CreateLicensePlateCommand command, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(command.Identifier, cancellationToken);
            if (licensePlate is not null)
                return CommandResponse.BadRequest("The license plate with given identifier already exists.");

            licensePlate = new LicensePlate { Identifier = command.Identifier };

            var validation = await _licensePlateValidator.ValidateAsync(licensePlate, cancellationToken);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _licensePlateRepository.Add(licensePlate);

            var saved = await _licensePlateRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }

        public async Task<ICommandResponse> CreateMotorVehicleAsync(CreateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumber(command.ChassisNumber, cancellationToken);
            if (motorVehicle is not null) 
                return CommandResponse.BadRequest("The given vehicle already exists");

            motorVehicle = new MotorVehicle
            {
                 Brand = command.Brand,
                 Model = command.Model,
                 BodyType = command.BodyType,
                 ChassisNumber = command.ChassisNumber,
                 Operational = command.Operational,
                 PropulsionType = command.PropulsionType
            };

            var validation = await _motorVehicleValidator.ValidateAsync(motorVehicle, cancellationToken);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _motorVehicleRepository.Add(motorVehicle);

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }

        public async Task<ICommandResponse> DeleteLicensePlateAsync(DeleteLicensePlateCommand command, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(command.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return CommandResponse.BadRequest("Could not find the license plate with given id.");
            if (licensePlate.InUse)
                return CommandResponse.BadRequest("The given license plate is in use. Please deactive the license plate before removing.");

            _licensePlateRepository.Remove(licensePlate);

            var saved = await _licensePlateRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }

        public async Task<ICommandResponse> WithdrawLicensePlateFromMotorVehicleAsync(WithdrawLicensePlateCommand command, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdAsync(command.LicensePlateId, cancellationToken);
            if (licensePlate is null)
                return CommandResponse.BadRequest("Could not find the license plate with given id.");
            if (licensePlate.InUse)
                return CommandResponse.BadRequest("The given license plate is in use. Please deactive the license plate before removing.");

            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdAsync(licensePlate.Id, cancellationToken);
            if (motorVehicle is null)
                return CommandResponse.NoContent();

            motorVehicle.LicensePlates.Remove(licensePlate);

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }
    }
}
