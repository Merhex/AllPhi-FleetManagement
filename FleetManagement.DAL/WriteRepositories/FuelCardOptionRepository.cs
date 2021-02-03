using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class FuelCardOptionRepository : Repository<FuelCardOption, int>, IFuelCardOptionRepository
    {
        public FuelCardOptionRepository(FleetManagementContext context) : base(context) { }

        public async Task<FuelCardOption> FindByNameAsync(string optionName, CancellationToken cancellationToken) =>
            await _context.FuelCardOptions
                .SingleOrDefaultAsync(option => option.Name == optionName, cancellationToken);
    }
}
