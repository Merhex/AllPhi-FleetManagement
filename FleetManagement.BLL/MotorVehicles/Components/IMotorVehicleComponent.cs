﻿using FleetManagement.BLL.MotorVehicles.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Components.Interfaces
{
    public interface IMotorVehicleComponent
    {
        Task<IComponentResponse> AssignLicensePlateToMotorVehicleAsync(IAssignLicensePlateContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> WithdrawLicensePlateFromMotorVehicleAsync(IWithdrawLicensePlateContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> DeleteLicensePlateAsync(IDeleteLicensePlateContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> CreateMotorVehicleAsync(ICreateMotorVehicleContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> CreateLicensePlateAsync(ICreateLicensePlateContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> ActivateLicensePlateAsync(IActivateLicensePlateContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> DeactivateLicensePlateAsync(IDeactivateLicensePlateContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> ActivateMotorVehicleAsync(IActivateMotorVehicle contract, CancellationToken cancellationToken);
        Task<IComponentResponse> DeactivateMotorVehicleAsync(IDeactivateMotorVehicle contract, CancellationToken cancellationToken);
    }
}
