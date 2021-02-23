using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface ITotalEntityCounter
    {
        Task<int> GetTotalCount<T>(CancellationToken cancellationToken = default, params Expression<Func<T, bool>>[] filters) where T : class;
    }
}
