using FleetManagement.BLL.Commands;
using FleetManagement.Mappings;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components.Interfaces
{
    public interface IDriverComponent
    {
        Task<CommandResponse> CreateDriverAsync(CreateDriverCommand command, CancellationToken token);
        Task<CommandResponse> UpdateDriverAsync(UpdateDriverInformationCommand command, CancellationToken token);
        Task<CommandResponse> ChangeDriverActitvityAsync(ChangeDriverActivityStatusCommand command, CancellationToken token);
    }
}
