using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface ILicensePlateRepository : IRepository<LicensePlate, int>
    {
        Task<LicensePlate> FindByIdentifierAsync(string identifier, CancellationToken cancellationToken);
        Task<LicensePlate> FindByIdAsync(int licensePlateId, CancellationToken cancellationToken);
    }
}
