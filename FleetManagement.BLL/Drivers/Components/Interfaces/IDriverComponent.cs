using FleetManagement.BLL.Drivers.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.Components.Interfaces
{
    public interface IDriverComponent
    {
        Task<IComponentResponse> CreateDriverAsync(ICreateDriverContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> ActivateDriverAsync(IActivateDriverContract contract, CancellationToken cancellationToken);
        Task<IComponentResponse> DeactivateDriverAsync(IDeactivateDriverContract contract, CancellationToken cancellationToken);
    }
}
