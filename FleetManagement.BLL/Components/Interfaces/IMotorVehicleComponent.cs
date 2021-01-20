using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Commands.Response;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components.Interfaces
{
    public interface IMotorVehicleComponent
    {
        Task<ICommandResponse> CreateMotorVehicle(CreateMotorVehicleCommand command, CancellationToken token);
    }
}
