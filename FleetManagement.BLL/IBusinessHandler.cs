using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public interface IBusinessHandler
    {
        Task<IBusinessHandlerResponse> Validate<T>(T contact, CancellationToken cancellationToken = default) where T : IContract;
    }
}
