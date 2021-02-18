using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IRepository<T, TKey>
            where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
