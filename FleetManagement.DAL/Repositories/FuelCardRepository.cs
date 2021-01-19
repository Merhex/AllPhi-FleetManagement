using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class FuelCardRepository : Repository<FuelCard, int>, IFuelCardRepository
    {
        public FuelCardRepository(FleetManagementContext context) : base(context) { }

        public async Task<FuelCard> FindByCardNumberAsync(string cardNumber) =>
            await _context.FuelCards.SingleOrDefaultAsync(card => card.CardNumber == cardNumber);
    }
}
