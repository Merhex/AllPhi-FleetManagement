using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
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

        public async Task<IComponentResponse> AssignLicensePlateToMotorVehicleAsync(IAssignLicensePlateContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var licensePlate = await GetUniqueLicensePlate(contract.LicensePlateIdentifier, response, token);

            var motorVehicle = await GetUniqueMotorVehicle(contract.ChassisNumber, response, token);

            LicensePlatePlateMustNotBeInUse(licensePlate, response);

            await LicensePlateMustNotBeAssignedToAnotherMotorVehicle(contract, response, token);

            AssignLicensePlateToMotorVehicle(motorVehicle, licensePlate);

            await Persistance(response);

            return response;
        }

        public async Task<IComponentResponse> ChangeLicensePlateInUseStatusAsync(IChangeLicensePlateInUseStatusContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var licensePlate = await GetUniqueLicensePlate(contract.Identifier, response, token);

            var motorVehicle = await GetMotorVehicleByLicensePlate(licensePlate.Identifier, response, token);

            ChangeLicensePlateUseCase(licensePlate, motorVehicle, contract.Status, response);

            CreateLicensePlateHistorySnapshot(licensePlate, motorVehicle, response);

            await Persistance(response);

            return response;
        }

        public async Task<IComponentResponse> ChangeMotorVehicleOperationalStatusAsync(IChangeMotorVehicleOperationalStatusContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var motorVehicle = await GetUniqueMotorVehicle(contract.ChassisNumber, response, token);

            ChangeMotorVehicleOperationalStatus(motorVehicle, contract.Operational);

            await Persistance(response);

            return response;
        }

        public async Task<IComponentResponse> CreateLicensePlateAsync(ICreateLicensePlateContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            await LicensePlateMustNotExist(contract.Identifier, response, token);

            var licensePlate = CreateLicensePlate(contract);

            await LicensePlateValidation(licensePlate, response, token);

            await Persistance(licensePlate, response);

            return response;
        }

        public async Task<IComponentResponse> CreateMotorVehicleAsync(ICreateMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberIncludeLicensePlatesAsync(contract.ChassisNumber, cancellationToken);
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

        #region PRIVATE
        private async Task LicensePlateValidation(LicensePlate licensePlate, ComponentResponse response, CancellationToken token)
        {
            var validation = await _licensePlateValidator.ValidateAsync(licensePlate, token);
            if (validation.IsValid is not true)
                response.ValidationFailure(validation);
            else
                response.Ok();
        }

        private static void ChangeMotorVehicleOperationalStatus(MotorVehicle motorVehicle, bool status)
        {
            motorVehicle.Operational = status;
        }

        private async Task LicensePlateMustBeAssigned(string identifier, ComponentResponse response, CancellationToken token)
        {
            var motorVehicle = await GetMotorVehicleByLicensePlate(identifier, response, token);

            if (motorVehicle is null)
                response.Ok();
            else
                response.BadRequest().WithTitle("The license plate is already assigned to another vehicle. Please withdraw the plate first.");
        }

        private void CreateLicensePlateHistorySnapshot(LicensePlate licensePlate, MotorVehicle motorVehicle, ComponentResponse response)
        {
            if (response.Valid is not true) return;

            var snapshot = new LicensePlateSnapshot
            {
                InUse = licensePlate.InUse,
                LicensePlate = licensePlate,
                MotorVehicle = motorVehicle,
                SnapshotDate = DateTime.Now
            };

            _licensePlateSnaphotRepository.Add(snapshot);
        }

        private static void AssignLicensePlateToMotorVehicle(MotorVehicle motorVehicle, LicensePlate licensePlate)
        {
            if (motorVehicle is null) return;
            if (licensePlate is null) return;

            motorVehicle.LicensePlates.Add(licensePlate);
        }

        private async Task<MotorVehicle> GetUniqueMotorVehicle(string chassisNumber, ComponentResponse response, CancellationToken token) 
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberIncludeLicensePlatesAsync(chassisNumber, token);

            if (motorVehicle is null)
                response.NotFound();
            else
                response.Ok();

            return motorVehicle;
        }

        private async Task<LicensePlate> GetUniqueLicensePlate(string identifier, ComponentResponse response, CancellationToken token)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(identifier, token);
            if (licensePlate is null)
                response.NotFound();
            else
                response.Ok();

            return licensePlate;
        }

        private LicensePlate CreateLicensePlate(ICreateLicensePlateContract contract)
        {
            var licensePlate = new LicensePlate
            {
                Identifier = contract.Identifier,
                InUse = false
            };

            return licensePlate;
        }

        private async Task LicensePlateMustNotExist(string identifier, ComponentResponse response, CancellationToken token)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(identifier, token);
            if (licensePlate is not null)
                response.AlreadyExists();
            else
                response.Ok();
        }

        private async Task<MotorVehicle> GetMotorVehicleByLicensePlate(string identifier, ComponentResponse response, CancellationToken token)
        {
            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdentifierIncludeLicensePlatesAsync(identifier, token);
            if (motorVehicle is null)
                response.NotFound();
            else
                response.Ok();

            return motorVehicle;
        }

        private async Task LicensePlateMustNotBeAssignedToAnotherMotorVehicle(IAssignLicensePlateContract contract, ComponentResponse response, CancellationToken token)
        {
            var motorVehicle = await GetMotorVehicleByLicensePlate(contract.LicensePlateIdentifier, response, token);

            if (motorVehicle.ChassisNumber == contract.ChassisNumber)
                response.Ok();
            else
                response.BadRequest().AddErrorMessage("The license plate is already assigned to another vehicle. Please withdraw the place first.");
        }

        private static void LicensePlatePlateMustNotBeInUse(LicensePlate licensePlate, ComponentResponse response)
        {
            if (licensePlate.InUse)
                response.BadRequest().AddErrorMessage("The license plate is in use, please put the license plate out of use first.");
            else
                response.Ok();
        }

        private static void ChangeLicensePlateUseCase(LicensePlate licensePlate, MotorVehicle motorVehicle, bool status, ComponentResponse response)
        {
            if (licensePlate is null) return;
            if (motorVehicle is null) return;

            var anyLicensePlateInUse = motorVehicle.LicensePlates
                             .Where(licensePlate => licensePlate.InUse)
                             .Any();

            if (anyLicensePlateInUse is true && status is true)
                response.BadRequest().AddErrorMessage("There is currently another license plate already in use.");
            else
                response.Ok();

            licensePlate.InUse = status;
        }

        private async Task<bool> Persistance(ComponentResponse response)
        {
            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
            {
                response.PersistanceFailure();
                return false;
            }
            response.Ok();
            return true;
        }

        private async Task Persistance(LicensePlate licensePlate, ComponentResponse response)
        {
            if (licensePlate is null) return;

            _licensePlateRepository.Add(licensePlate);

            await Persistance(response);
        }
        #endregion
    }
}
