using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public interface IBusinessRule
    {
        Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default);
    }
}
