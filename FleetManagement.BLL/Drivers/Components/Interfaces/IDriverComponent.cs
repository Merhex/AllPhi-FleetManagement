using FleetManagement.BLL.Drivers.Commands;
using FleetManagement.BLL.Shared.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.Components.Interfaces
{
    public interface IDriverComponent
    {
        Task<ICommandResponse> CreateDriverAsync(CreateDriverCommand command, CancellationToken token);
        Task<ICommandResponse> UpdateDriverAsync(UpdateDriverInformationCommand command, CancellationToken token);
        Task<ICommandResponse> ChangeDriverActitvityAsync(ChangeDriverActivityStatusCommand command, CancellationToken token);
    }
}
