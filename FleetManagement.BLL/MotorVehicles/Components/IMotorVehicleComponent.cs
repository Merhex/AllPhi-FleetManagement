using FleetManagement.BLL.MotorVehicles.Contracts;
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
        Task<IComponentResponse> ChangeLicensePlateInUseStatusAsync(IChangeLicensePlateInUseStatusContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> ChangeMotorVehicleOperationalStatusAsync(IChangeMotorVehicleOperationalStatusContract contract, CancellationToken cancellationToken);
    }
}
