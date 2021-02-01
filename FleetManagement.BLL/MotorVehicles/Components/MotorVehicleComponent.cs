using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;

namespace FleetManagement.BLL.MotorVehicles.Components
{
    public class MotorVehicleComponent : IMotorVehicleComponent
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;
        private readonly ILicensePlateRepository _licensePlateRepository;
        private readonly ILicensePlateSnapshotRepository _licensePlateSnaphotRepository;
        private readonly IBusinessHandler _businessHandler;

        public MotorVehicleComponent(
            IMotorVehicleRepository motorVehicleRepository,
            ILicensePlateRepository licensePlateRepository,
            ILicensePlateSnapshotRepository licensePlateSnaphotRepository,
            IBusinessHandler businessHandler)
        {
            _motorVehicleRepository = motorVehicleRepository;
            _licensePlateRepository = licensePlateRepository;
            _licensePlateSnaphotRepository = licensePlateSnaphotRepository;
            _businessHandler = businessHandler;
        }

        //public async Task<IComponentResponse> AssignLicensePlateToMotorVehicleAsync(IAssignLicensePlateContract contract, CancellationToken token)
        //{
        //    var response = new ComponentResponse();

        //    var licensePlate = await GetUniqueLicensePlate(contract.LicensePlateIdentifier, response, token);

        //    var motorVehicle = await GetUniqueMotorVehicle(contract.ChassisNumber, response, token);

        //    LicensePlateMustNotBeInUse(licensePlate, response);

        //    await LicensePlateMustNotBeAssignedToAnotherMotorVehicle(contract, response, token);

        //    AssignLicensePlateToMotorVehicle(motorVehicle, licensePlate, response);

        //    await Persistance(response);

        //    return response;
        //}

        //public async Task<IComponentResponse> ChangeLicensePlateInUseStatusAsync(IChangeLicensePlateInUseStatusContract contract, CancellationToken token)
        //{
        //    var response = new ComponentResponse();

        //    var licensePlate = await GetUniqueLicensePlate(contract.Identifier, response, token);

        //    var motorVehicle = await GetMotorVehicleByLicensePlate(licensePlate.Identifier, response, token);

        //    LicensePlateToBeChangedMustBeAssignedToMotorVehicle(motorVehicle, response);

        //    AnotherLicensePlateMustNotBeInUseOnMotorVehicle(motorVehicle, contract.Status, response);

        //    ChangeLicensePlateUseCase(licensePlate, contract.Status, response);

        //    CreateLicensePlateHistorySnapshot(licensePlate, motorVehicle, response);

        //    await Persistance(response);

        //    return response;
        //}

        //public async Task<IComponentResponse> ChangeMotorVehicleOperationalStatusAsync(IChangeMotorVehicleOperationalStatusContract contract, CancellationToken token)
        //{
        //    var response = new ComponentResponse();

        //    var motorVehicle = await GetUniqueMotorVehicle(contract.ChassisNumber, response, token);

        //    ChangeMotorVehicleOperationalStatus(motorVehicle, contract.Operational);

        //    await Persistance(response);

        //    return response;
        //}

        public async Task<IComponentResponse> CreateLicensePlateAsync(ICreateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await CreateNewLicensePlateBasedOn(contract);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> CreateMotorVehicleAsync(ICreateMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await CreateMotorVehicleBasedOn(contract);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> AssignLicensePlateToMotorVehicleAsync(IAssignLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await AssignLicensePlateToMotorVehicleBasedOn(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> WithdrawLicensePlateFromMotorVehicleAsync(IWithdrawLicensePlateContract contract, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IComponentResponse> DeleteLicensePlateAsync(IDeleteLicensePlateContract contract, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IComponentResponse> ChangeLicensePlateInUseStatusAsync(IChangeLicensePlateInUseStatusContract contract, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IComponentResponse> ChangeMotorVehicleOperationalStatusAsync(IChangeMotorVehicleOperationalStatusContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await ChangeMotorVehicleOperationalStatusBasedOn(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        #region PRIVATE
        private async Task AssignLicensePlateToMotorVehicleBasedOn(IAssignLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberIncludeLicensePlatesAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.LicensePlates.Add(licensePlate);

            await _motorVehicleRepository.SaveAsync();
        }

        private async Task CreateNewLicensePlateBasedOn(ICreateLicensePlateContract contract)
        {
            var licensePlate = new LicensePlate { Identifier = contract.Identifier };

            _licensePlateRepository.Add(licensePlate);

            await _licensePlateRepository.SaveAsync();
        }

        private async Task CreateMotorVehicleBasedOn(ICreateMotorVehicleContract contract)
        {
            var motorVehicle = new MotorVehicle
            {
                BodyType = (MotorVehicleBodyType)contract.BodyType,
                Brand = contract.Brand,
                ChassisNumber = contract.ChassisNumber,
                Model = contract.Model,
                Operational = contract.Operational,
                PropulsionType = (MotorVehiclePropulsionType)contract.PropulsionType
            };

            _motorVehicleRepository.Add(motorVehicle);

            await _motorVehicleRepository.SaveAsync();
        }

        private async Task ChangeMotorVehicleOperationalStatusBasedOn(IChangeMotorVehicleOperationalStatusContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.Operational = contract.Operational;

            await _motorVehicleRepository.SaveAsync();
        }
        #endregion
    }
}
