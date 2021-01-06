using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagment.DAL.Repositories.Interfaces
{
    public interface IRepository<T, TKey>
            where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entitiy);
        void UpdateRange(IEnumerable<T> entities);
        Task<T> FindAsync(TKey key);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> AllAsync();
        Task<bool> SaveAsync();
    }
}
