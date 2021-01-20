using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Commands.Response;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components.Interfaces
{
    public interface IFuelCardComponent
    {
        public Task<ICommandResponse> CreateFuelCardAsync(CreateFuelCardCommand command, CancellationToken cancellationToken);
        public Task<ICommandResponse> AddFuelCardOptionsAsync(AddFuelCardOptionsCommand command, CancellationToken cancellationToken);
    }
}
