using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public interface IBusinessRuleValidator<T> where T : IContract
    {
        Task<IBusinessRuleListenerResponse> Validate(T contract, CancellationToken token = default);
    }
}
