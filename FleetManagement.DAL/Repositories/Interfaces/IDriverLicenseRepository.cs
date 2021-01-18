using FleetManagement.Models;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IDriverLicenseRepository : IRepository<DriverLicense, int>
    {
        Task<DriverLicense> FindLicenseByIdentifier(string identifier);
    }
}
