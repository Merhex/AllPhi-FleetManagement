using FleetManagement.Models;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IDriverLicenseRepository
    {
        Task<DriverLicense> FindLicenseByIdentifier(string identifier);
    }
}
