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
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await WithdrawLicensePlateBasedOn(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> DeleteLicensePlateAsync(IDeleteLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await DeleteLicensePlateBasedOn(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> ActivateLicensePlateAsync(IActivateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await ActivateLicensePlate(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> DeactivateLicensePlateAsync(IDeactivateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await DeactivateLicensePlate(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> ActivateMotorVehicleAsync(IActivateMotorVehicle contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await ActivateMotorVehicle(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> DeactivateMotorVehicleAsync(IDeactivateMotorVehicle contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await DeactivateMotorVehicle(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        #region PRIVATE
        private async Task DeactivateLicensePlate(IDeactivateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);
            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdentifierAsync(contract.Identifier, cancellationToken);

            licensePlate.InUse = false;

            var snapshot = new LicensePlateSnapshot
            {
                LicensePlate = licensePlate,
                MotorVehicle = motorVehicle,
                InUse = false,
                SnapshotDate = DateTime.Now
            };

            _licensePlateSnaphotRepository.Add(snapshot);

            await _licensePlateRepository.SaveAsync();
        }

        private async Task DeleteLicensePlateBasedOn(IDeleteLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);

            _licensePlateRepository.Remove(licensePlate);

            await _licensePlateRepository.SaveAsync();
        }

        private async Task ActivateLicensePlate(IActivateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);
            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdentifierAsync(contract.Identifier, cancellationToken);

            licensePlate.InUse = true;

            var snapshot = new LicensePlateSnapshot 
            { 
                LicensePlate = licensePlate,
                MotorVehicle = motorVehicle, 
                InUse = true, 
                SnapshotDate = DateTime.Now
            };

            _licensePlateSnaphotRepository.Add(snapshot);

            await _licensePlateRepository.SaveAsync();
        }

        private async Task WithdrawLicensePlateBasedOn(IWithdrawLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdentifierIncludeLicensePlatesAsync(contract.Identifier, cancellationToken);
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);

            motorVehicle.LicensePlates.Remove(licensePlate);

            await _motorVehicleRepository.SaveAsync();
        }

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

        private async Task ActivateMotorVehicle(IActivateMotorVehicle contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.Operational = true;

            await _motorVehicleRepository.SaveAsync();
        }

        private async Task DeactivateMotorVehicle(IDeactivateMotorVehicle contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.Operational = false;

            await _motorVehicleRepository.SaveAsync();
        }
        #endregion
    }
}
