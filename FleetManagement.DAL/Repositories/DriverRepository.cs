using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class DriverRepository : Repository<Driver, int>, IDriverRepository
    {
        public DriverRepository(FleetManagementContext context) : base (context) { }

        public async Task<IEnumerable<Driver>> FindAllDrivers() =>
            await _context.Drivers
                .ToListAsync();

        public async Task<Driver> FindDriverByNationalNumber(string nationalNumber) =>
            await _context.Drivers
                .Include(d => d.DriverLicense)
                .SingleOrDefaultAsync(d => d.NationalNumber == nationalNumber);
           
    }
}
