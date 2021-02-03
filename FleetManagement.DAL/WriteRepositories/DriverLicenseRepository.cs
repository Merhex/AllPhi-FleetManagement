using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class DriverLicenseRepository : Repository<DriverLicense, int>, IDriverLicenseRepository
    {
        public DriverLicenseRepository(FleetManagementContext context) : base(context) { }

        public async Task<DriverLicense> FindLicenseByIdentifier(string identifier) =>
            await _context.DriverLicenses
                .SingleOrDefaultAsync(dl => dl.Identifier == identifier);
    }
}
