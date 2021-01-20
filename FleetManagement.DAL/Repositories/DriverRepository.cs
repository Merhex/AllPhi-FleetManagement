using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class DriverRepository : Repository<Driver, int>, IDriverRepository
    {
        public DriverRepository(FleetManagementContext context) : base (context) { }

        public async Task<IEnumerable<Driver>> FindAllDrivers(CancellationToken cancellationToken) =>
            await _context.Drivers
                .ToListAsync(cancellationToken);

        public async Task<Driver> FindByIdAsync(int driverId, CancellationToken cancellationToken) =>
            await _context.Drivers
                .Include(d => d.DriverLicense)
                .SingleOrDefaultAsync(d => d.Id == driverId, cancellationToken);

        public async Task<Driver> FindDriverByNationalNumberAsync(string nationalNumber, CancellationToken cancellationToken) =>
            await _context.Drivers
                .Include(d => d.DriverLicense)
                .SingleOrDefaultAsync(d => d.NationalNumber == nationalNumber, cancellationToken);
           
    }
}
