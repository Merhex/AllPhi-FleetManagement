using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> FindByNationalNumberAsync(string nationalNumber, CancellationToken cancellationToken);
    }
}
