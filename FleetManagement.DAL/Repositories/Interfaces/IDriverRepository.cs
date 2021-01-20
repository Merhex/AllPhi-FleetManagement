using FleetManagement.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IDriverRepository : IRepository<Driver, int>
    {
        Task<Driver> FindDriverByNationalNumberAsync(string nationalNumber, CancellationToken cancellationToken);
        Task<Driver> FindByIdAsync(int driverId, CancellationToken cancellationToken);
        Task<IEnumerable<Driver>> FindAllDrivers(CancellationToken cancellationToken);
    }
}
