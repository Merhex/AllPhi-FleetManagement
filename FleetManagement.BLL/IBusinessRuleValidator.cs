using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public interface IBusinessRuleValidator
    {
        Task<IBusinessRuleListenerResponse> Validate<T>(T contract, CancellationToken token = default) where T : IContract;
    }
}
