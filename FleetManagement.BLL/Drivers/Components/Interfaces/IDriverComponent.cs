using FleetManagement.BLL.Drivers.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.Components.Interfaces
{
    public interface IDriverComponent
    {
        Task<IComponentResponse> CreateDriverAsync(ICreateDriverContract contract, CancellationToken token);
        Task<IComponentResponse> ChangeDriverActivityAsync(IChangeDriverActivityStatusContract contract, CancellationToken token);
    }
}
