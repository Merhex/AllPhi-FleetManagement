using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public interface IBusinessRule<T> where T : IContract
    {
        event BusinessRuleFailureEventHandler<T> Failure;
        Task Handle(T contract, CancellationToken token = default);
    }
}
