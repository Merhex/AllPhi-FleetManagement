using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class LicensePlateRepository : Repository<LicensePlate, int>, ILicensePlateRepository
    {
        public LicensePlateRepository(FleetManagementContext context) : base(context) { }

        public async Task<LicensePlate> FindByIdentifierAsync(string identifier, CancellationToken cancellationToken) =>
            await _context.LicensePlates
                .SingleOrDefaultAsync(licensePlate => licensePlate.Identifier == identifier, cancellationToken);
    }
}
