using FleetManagement.BLL.MotorVehicles.Commands;
using FleetManagement.BLL.Shared.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Components.Interfaces
{
    public interface IMotorVehicleComponent
    {
        Task<ICommandResponse> CreateMotorVehicleAsync(CreateMotorVehicleCommand command, CancellationToken token);
        Task<ICommandResponse> CreateLicensePlateAsync(CreateLicensePlateCommand command, CancellationToken token);
        Task<ICommandResponse> ChangeOperationalStatusAsync(ChangeMotorVehicleOperationalStatusCommand command, CancellationToken token);
    }
}
