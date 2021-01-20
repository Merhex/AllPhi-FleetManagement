using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Commands.Response;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components.Interfaces
{
    public interface IDriverComponent
    {
        Task<ICommandResponse> CreateDriverAsync(CreateDriverCommand command, CancellationToken token);
        Task<ICommandResponse> UpdateDriverAsync(UpdateDriverInformationCommand command, CancellationToken token);
        Task<ICommandResponse> ChangeDriverActitvityAsync(ChangeDriverActivityStatusCommand command, CancellationToken token);
    }
}
