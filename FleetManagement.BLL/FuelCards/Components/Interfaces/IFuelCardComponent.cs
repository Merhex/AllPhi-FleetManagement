using FleetManagement.BLL.FuelCards.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.FuelCards.Components.Interfaces
{
    public interface IFuelCardComponent
    {
        public Task<IComponentResponse> CreateFuelCardAsync(ICreateFuelCardContract contract, CancellationToken cancellationToken);
        public Task<IComponentResponse> AddFuelCardOptionsAsync(IAddFuelCardOptionsContract contract, CancellationToken cancellationToken);
        public Task<IComponentResponse> DeleteFuelCardAsync(IDeleteFuelCardContract contract, CancellationToken cancellationToken);
    }
}
