using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface ITotalEntityCounter
    {
        Task<int> GetTotalCount<T>(params Expression<Func<T, bool>>[] filters) where T : class;
    }
}
