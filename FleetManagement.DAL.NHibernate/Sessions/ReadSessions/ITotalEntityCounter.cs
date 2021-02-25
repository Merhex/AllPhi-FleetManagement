using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public interface ITotalEntityCounter<T>
    {
        Task<int> GetTotalCount(CancellationToken cancellationToken = default, params Expression<Func<T, bool>>[] filters);
    }
}
