using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IFuelCardRepository
    {
        Task<FuelCard> FindByCardNumberAsync(string cardNumber, CancellationToken cancellationToken);
        Task<FuelCard> FindByIdAsync(int fuelCardId, CancellationToken cancellationToken);
    }
}
