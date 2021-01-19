using FleetManagement.BLL.Commands;
using FleetManagement.Mappings;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components.Interfaces
{
    public interface IFuelCardComponent
    {
        public Task<CommandResponse> CreateFuelCardAsync(CreateFuelCardCommand command, CancellationToken cancellationToken);
    }
}
