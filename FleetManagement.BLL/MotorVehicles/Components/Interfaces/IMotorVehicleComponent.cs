using FleetManagement.BLL.MotorVehicles.Commands;
using FleetManagement.BLL.Shared.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Components.Interfaces
{
    public interface IMotorVehicleComponent
    {
        Task<ICommandResponse> AttachLicensePlateToMotorVehicleAsync(AttachLicensePlateCommand command, CancellationToken cancellationToken);
        Task<ICommandResponse> DetachLicensePlateFromMotorVehicleAsync(DetachLicensePlaceCommand command, CancellationToken cancellationToken);
        Task<ICommandResponse> DeleteLicensePlateAsync(DeleteLicensePlateCommand command, CancellationToken cancellationToken);
        Task<ICommandResponse> CreateMotorVehicleAsync(CreateMotorVehicleCommand command, CancellationToken cancellationToken);
        Task<ICommandResponse> CreateLicensePlateAsync(CreateLicensePlateCommand command, CancellationToken cancellationToken);
        Task<ICommandResponse> ChangeLicensePlateInUseStatusAsync(ChangeLicensePlateInUseStatusCommand command, CancellationToken cancellationToken);
        Task<ICommandResponse> ChangeMotorVehicleOperationalStatusAsync(ChangeMotorVehicleOperationalStatusCommand command, CancellationToken cancellationToken);
    }
}
