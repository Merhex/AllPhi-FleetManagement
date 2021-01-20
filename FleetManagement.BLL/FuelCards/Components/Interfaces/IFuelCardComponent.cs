using FleetManagement.BLL.FuelCards.Commands;
using FleetManagement.BLL.Shared.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.FuelCards.Components.Interfaces
{
    public interface IFuelCardComponent
    {
        public Task<ICommandResponse> CreateFuelCardAsync(CreateFuelCardCommand command, CancellationToken cancellationToken);
        public Task<ICommandResponse> AddFuelCardOptionsAsync(AddFuelCardOptionsCommand command, CancellationToken cancellationToken);
        public Task<ICommandResponse> DeleteFuelCardAsync(DeleteFuelCardCommand command, CancellationToken cancellationToken);
    }
}
