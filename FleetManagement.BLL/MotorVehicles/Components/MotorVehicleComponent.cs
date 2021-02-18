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
        private readonly IMotorVehicleMileageSnapshotRepository _motorVehicleMileageSnapshotRepository;
        private readonly IBusinessHandler _businessHandler;

        public MotorVehicleComponent(
            IMotorVehicleRepository motorVehicleRepository,
            ILicensePlateRepository licensePlateRepository,
            ILicensePlateSnapshotRepository licensePlateSnaphotRepository,
            IMotorVehicleMileageSnapshotRepository motorVehicleMileageSnapshotRepository,
            IBusinessHandler businessHandler)
        {
            _motorVehicleRepository = motorVehicleRepository;
            _licensePlateRepository = licensePlateRepository;
            _licensePlateSnaphotRepository = licensePlateSnaphotRepository;
            _businessHandler = businessHandler;
            _motorVehicleMileageSnapshotRepository = motorVehicleMileageSnapshotRepository;
        }

        public async Task<IComponentResponse> CreateLicensePlateAsync(ICreateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await CreateLicensePlate(contract, cancellationToken);

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
                await CreateMotorVehicle(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> UpdateMotorVehicleAsync(IUpdateMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await UpdateMotorVehicle(contract, cancellationToken);

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
                await AssignLicensePlateToMotorVehicle(contract, cancellationToken);

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
                await WithdrawLicensePlate(contract, cancellationToken);

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
                await DeleteLicensePlate(contract, cancellationToken);

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

        public async Task<IComponentResponse> ActivateMotorVehicleAsync(IActivateMotorVehicleContract contract, CancellationToken cancellationToken)
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

        public async Task<IComponentResponse> DeactivateMotorVehicleAsync(IDeactivateMotorVehicleContract contract, CancellationToken cancellationToken)
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

        public async Task<IComponentResponse> AddMileageToMotorVehicleAsync(IAddMileageToMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await AddMileageToMotorVehicle(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        #region PRIVATE
        private async Task AddMileageToMotorVehicle(IAddMileageToMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, cancellationToken);

            var snapshot = new MotorVehicleMileageSnapshot
            {
                Mileage = contract.Mileage,
                MotorVehicle = motorVehicle,
                SnapshotDate = contract.Date
            };

            motorVehicle.MileageHistory.Add(snapshot);

            await _motorVehicleRepository.SaveAsync(cancellationToken);
        }

        private async Task UpdateMotorVehicle(IUpdateMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.BodyType       = (MotorVehicleBodyType) contract.BodyType;
            motorVehicle.PropulsionType = (MotorVehiclePropulsionType) contract.PropulsionType;
            motorVehicle.Operational    = contract.Operational;
            motorVehicle.Model          = contract.Model;
            motorVehicle.Brand          = contract.Brand;

            await _motorVehicleRepository.SaveAsync(cancellationToken);
        }

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

            await _licensePlateRepository.SaveAsync(cancellationToken);
        }

        private async Task DeleteLicensePlate(IDeleteLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);

            _licensePlateRepository.Remove(licensePlate);

            await _licensePlateRepository.SaveAsync(cancellationToken);
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

            await _licensePlateRepository.SaveAsync(cancellationToken);
        }

        private async Task WithdrawLicensePlate(IWithdrawLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByLicensePlateIdentifierIncludeLicensePlatesAsync(contract.Identifier, cancellationToken);
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);

            motorVehicle.LicensePlates.Remove(licensePlate);

            await _motorVehicleRepository.SaveAsync(cancellationToken);
        }

        private async Task AssignLicensePlateToMotorVehicle(IAssignLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, cancellationToken);
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberIncludeLicensePlatesAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.LicensePlates.Add(licensePlate);

            await _motorVehicleRepository.SaveAsync(cancellationToken);
        }

        private async Task CreateLicensePlate(ICreateLicensePlateContract contract, CancellationToken cancellationToken)
        {
            var licensePlate = new LicensePlate { Identifier = contract.Identifier };

            _licensePlateRepository.Add(licensePlate);

            await _licensePlateRepository.SaveAsync(cancellationToken);
        }

        private async Task CreateMotorVehicle(ICreateMotorVehicleContract contract, CancellationToken cancellationToken)
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

            var mileage = new MotorVehicleMileageSnapshot
            {
                MotorVehicle = motorVehicle,
                Mileage = contract.Mileage,
                SnapshotDate = DateTime.Now
            };

            _motorVehicleRepository.Add(motorVehicle);
            await _motorVehicleRepository.SaveAsync(cancellationToken);

            _motorVehicleMileageSnapshotRepository.Add(mileage);
            await _motorVehicleMileageSnapshotRepository.SaveAsync(cancellationToken);
        }

        private async Task ActivateMotorVehicle(IActivateMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.Operational = true;

            await _motorVehicleRepository.SaveAsync(cancellationToken);
        }

        private async Task DeactivateMotorVehicle(IDeactivateMotorVehicleContract contract, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, cancellationToken);

            motorVehicle.Operational = false;

            await _motorVehicleRepository.SaveAsync(cancellationToken);
        }
        #endregion
    }
}
