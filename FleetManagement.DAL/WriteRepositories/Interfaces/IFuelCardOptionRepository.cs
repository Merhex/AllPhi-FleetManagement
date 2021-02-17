using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IFuelCardOptionRepository
    {
        Task<FuelCardOption> FindByNameAsync(string optionName, CancellationToken cancellationToken);
    }

}
