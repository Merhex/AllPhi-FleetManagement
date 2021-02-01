using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public interface IBusinessHandler<T> where T : IContract
    {
        IBusinessHandler<T> Read(T contract);
        Task<IBusinessHandlerResponse> Validate(CancellationToken cancellationToken = default);
    }
}
