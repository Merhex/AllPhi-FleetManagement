using FleetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IDriverRepository : IRepository<Driver, int>
    {
        Task<Driver> FindDriverByNationalNumberAsync(string nationalNumber);
        Task<Driver> FindByIdAsync(int driverId);
        Task<IEnumerable<Driver>> FindAllDrivers();
    }
}
