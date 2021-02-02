using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public interface IBusinessHandler<T> where T : IContract
    {
        Task<IBusinessHandlerResponse> Validate(T contract, CancellationToken cancellationToken = default);
    }
}
