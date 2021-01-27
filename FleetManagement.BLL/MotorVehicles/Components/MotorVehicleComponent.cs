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

            LicensePlateMustNotBeInUse(licensePlate, response);

            await LicensePlateMustNotBeAssignedToAnotherMotorVehicle(contract, response, token);

            AssignLicensePlateToMotorVehicle(motorVehicle, licensePlate, response);

            await Persistance(response);

            return response;
        }

        public async Task<IComponentResponse> ChangeLicensePlateInUseStatusAsync(IChangeLicensePlateInUseStatusContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var licensePlate = await GetUniqueLicensePlate(contract.Identifier, response, token);

            var motorVehicle = await GetMotorVehicleByLicensePlate(licensePlate.Identifier, response, token);

            LicensePlateToBeChangedMustBeAssignedToMotorVehicle(motorVehicle, response);

            AnotherLicensePlateMustNotBeInUseOnMotorVehicle(motorVehicle, contract.Status, response);

            ChangeLicensePlateUseCase(licensePlate, contract.Status, response);

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
            //var response = new ComponentResponse();

            //await LicensePlateMustNotExist(contract.Identifier, response, token);

            //var licensePlate = CreateLicensePlate(contract);

            //await LicensePlateValidation(licensePlate, response, token);

            //await Persistance(licensePlate, response);

            //return response;

            var businessRuleHandlerBuilder = new BusinessRuleHandlerBuilder<ICreateLicensePlateContract>();
        }

        public async Task<IComponentResponse> CreateMotorVehicleAsync(ICreateMotorVehicleContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            await MotorVehicleMustNotExist(contract.ChassisNumber, response, token);

            var motorVehicle = CreateMotorVehicle(contract);

            await MotorVehicleValidation(motorVehicle, response, token);

            await Persistance(motorVehicle, response);

            return response;
        }

        public async Task<IComponentResponse> DeleteLicensePlateAsync(IDeleteLicensePlateContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var licensePlate = await GetUniqueLicensePlate(contract.Identifier, response, token);

            LicensePlateMustNotBeInUse(licensePlate, response);

            DeleteLicensePlate(licensePlate);

            await Persistance(response);

            return response;
        }

        public async Task<IComponentResponse> WithdrawLicensePlateFromMotorVehicleAsync(IWithdrawLicensePlateContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var licensePlate = await GetUniqueLicensePlate(contract.Identifier, response, token);

            var motorVehicle = await GetMotorVehicleByLicensePlate(licensePlate.Identifier, response, token);

            LicensePlateMustNotBeInUse(licensePlate, response);

            WithdrawLicensePlateFromMotorVehicle(motorVehicle, licensePlate);

            await Persistance(response);

            return response;
        }

        #region PRIVATE
        private static void WithdrawLicensePlateFromMotorVehicle(MotorVehicle motorVehicle, LicensePlate licensePlate)
        {
            if (motorVehicle is null) return;
            if (licensePlate is null) return;

            motorVehicle.LicensePlates.Remove(licensePlate);
        }

        private void DeleteLicensePlate(LicensePlate licensePlate)
        {
            _licensePlateRepository.Remove(licensePlate);
        }

        private async Task MotorVehicleValidation(MotorVehicle motorVehicle, ComponentResponse response, CancellationToken token)
        {
            var validation = await _motorVehicleValidator.ValidateAsync(motorVehicle, token);
            if (validation.IsValid is not true)
                response.ValidationFailure(validation);
            else
                response.Ok();
        }

        private static MotorVehicle CreateMotorVehicle(ICreateMotorVehicleContract contract)
        {
            var motorVehicle = new MotorVehicle
            {
                PropulsionType = (MotorVehiclePropulsionType)contract.PropulsionType,
                BodyType = (MotorVehicleBodyType)contract.BodyType,
                Brand = contract.Brand,
                Model = contract.Model,
                ChassisNumber = contract.ChassisNumber,
                Operational = contract.Operational
            };

            return motorVehicle;
        }

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

        private static void LicensePlateToBeChangedMustBeAssignedToMotorVehicle(MotorVehicle motorVehicle, ComponentResponse response)
        {
            if (motorVehicle is not null)
                response.Ok();
            else
                response.BadRequest().AddErrorMessage("The license plate is not assigned to a vehicle. Please assign the plate first.");
        }
        
        private async Task MotorVehicleMustNotExist(string chassisNumber, ComponentResponse response, CancellationToken token)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberIncludeLicensePlatesAsync(chassisNumber, token);

            if (motorVehicle is not null)
                response.AlreadyExists(motorVehicle.ChassisNumber);
            else
                response.Ok();
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

        private static void AssignLicensePlateToMotorVehicle(MotorVehicle motorVehicle, LicensePlate licensePlate, ComponentResponse response)
        {
            if (response.Valid is not true) return;
            if (motorVehicle is null) return;
            if (licensePlate is null) return;
            if (motorVehicle.LicensePlates.Contains(licensePlate)) return;

            motorVehicle.LicensePlates.Add(licensePlate);
        }

        private async Task<MotorVehicle> GetUniqueMotorVehicle(string chassisNumber, ComponentResponse response, CancellationToken token) 
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberIncludeLicensePlatesAsync(chassisNumber, token);

            if (motorVehicle is null)
                response.NotFound(chassisNumber);
            else
                response.Ok();

            return motorVehicle;
        }

        private async Task<LicensePlate> GetUniqueLicensePlate(string identifier, ComponentResponse response, CancellationToken token)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(identifier, token);
            if (licensePlate is null)
                response.NotFound(identifier);
            else
                response.Ok();

            return licensePlate;
        }

        private static LicensePlate CreateLicensePlate(ICreateLicensePlateContract contract)
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
                response.AlreadyExists(licensePlate.Identifier);
            else
                response.Ok();
        }

        private async Task<MotorVehicle> GetMotorVehicleByLicensePlate(string identifier, ComponentResponse response, CancellationToken token)
        {
            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdentifierIncludeLicensePlatesAsync(identifier, token);
            if (motorVehicle is null)
                response.NotFound(identifier);
            else
                response.Ok();

            return motorVehicle;
        }

        private async Task LicensePlateMustNotBeAssignedToAnotherMotorVehicle(IAssignLicensePlateContract contract, ComponentResponse response, CancellationToken token)
        {
            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdentifierIncludeLicensePlatesAsync(contract.LicensePlateIdentifier, token);
            if (motorVehicle is null) return;

            if (motorVehicle.ChassisNumber == contract.ChassisNumber)
                response.Ok();
            else
                response.BadRequest().AddErrorMessage("The license plate is already assigned to another vehicle. Please withdraw the place first.");
        }

        private static void LicensePlateMustNotBeInUse(LicensePlate licensePlate, ComponentResponse response)
        {
            if (licensePlate is null) return;

            if (licensePlate.InUse)
                response.BadRequest().AddErrorMessage("The license plate is in use, please put the license plate out of use first.");
            else
                response.Ok();
        }

        private static void AnotherLicensePlateMustNotBeInUseOnMotorVehicle(MotorVehicle motorVehicle, bool inUse, ComponentResponse response)
        {
            if (motorVehicle is null) return;

            var licensePlateInUse = motorVehicle.LicensePlates
                             .Where(licensePlate => licensePlate.InUse)
                             .Any();

            if (licensePlateInUse && inUse is true)
                response.BadRequest().AddErrorMessage("There is currently another license plate already in use.");
            else
                response.Ok();

        }

        private static void ChangeLicensePlateUseCase(LicensePlate licensePlate, bool status, ComponentResponse response)
        {
            if (licensePlate is null) return;
            if (response.Valid is not true) return;

            licensePlate.InUse = status;
        }

        private async Task Persistance(ComponentResponse response)
        {
            if (response.Valid is not true) return;

            var saved = await _motorVehicleRepository.SaveAsync();

            if (saved is not true)
                response.PersistanceFailure();
            else
                response.Ok();
        }

        private async Task Persistance(LicensePlate licensePlate, ComponentResponse response)
        {
            if (licensePlate is null) return;

            _licensePlateRepository.Add(licensePlate);

            await Persistance(response);
        }

        private async Task Persistance(MotorVehicle motorVehicle, ComponentResponse response)
        {
            if (motorVehicle is null) return;

            _motorVehicleRepository.Add(motorVehicle);

            await Persistance(response);
        }
        #endregion
    }
}
