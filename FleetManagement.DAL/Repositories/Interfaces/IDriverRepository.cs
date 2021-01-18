using FleetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IDriverRepository : IRepository<Driver, int>
    {
        Task<Driver> FindDriverByNationalNumber(string nationalNumber);
        Task<IEnumerable<Driver>> FindAllDrivers();
    }
}
