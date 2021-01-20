using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class FuelCardRepository : Repository<FuelCard, int>, IFuelCardRepository
    {
        public FuelCardRepository(FleetManagementContext context) : base(context) { }

        public async Task<FuelCard> FindByCardNumberAsync(string cardNumber, CancellationToken cancellationToken) =>
            await _context.FuelCards
                .Include(card => card.Options)
                .SingleOrDefaultAsync(card => card.CardNumber == cardNumber, cancellationToken);

        public async Task<FuelCard> FindByIdAsync(int fuelCardId, CancellationToken cancellationToken) =>
            await _context.FuelCards
                .Include(card => card.Options)
                .SingleOrDefaultAsync(card => card.Id == fuelCardId, cancellationToken);
    }
}
