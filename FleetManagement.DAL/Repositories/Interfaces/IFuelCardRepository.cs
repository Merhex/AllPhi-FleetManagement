using FleetManagement.Models;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IFuelCardRepository : IRepository<FuelCard, int>
    {
        Task<FuelCard> FindByCardNumberAsync(string cardNumber);
    }

}
