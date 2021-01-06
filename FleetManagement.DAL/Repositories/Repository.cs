using FleetManagment.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagment.DAL.Repositories
{
    class Repository<T, TKey> : IRepository<T, TKey>
        where T : class
    {
        protected readonly ApplicationContext _context;

        public Repository()
        {
            _context = context;
        }

        public virtual void Add(T entity) =>
            _context.Set<T>().Add(entity);

        public virtual void AddRange(IEnumerable<T> entities) =>
            _context.Set<T>().AddRange(entities);

        public virtual async Task<IEnumerable<T>> AllAsync() =>
            await _context.Set<T>().ToListAsync();

        public virtual async Task<T> FindAsync(TKey key) =>
            await _context.Set<T>().FindAsync(key);

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _context.Set<T>().SingleOrDefaultAsync(predicate);

        public virtual async Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate) =>
             await _context.Set<T>().Where(predicate).ToListAsync();

        public virtual void Remove(T entity) =>
            _context.Set<T>().Remove(entity);

        public virtual void RemoveRange(IEnumerable<T> entities) =>
            _context.Set<T>().RemoveRange(entities);

        public virtual void Update(T entity) =>
            _context.Set<T>().Update(entity);

        public virtual void UpdateRange(IEnumerable<T> entities) =>
            _context.Set<T>().UpdateRange(entities);

        public virtual async Task<bool> SaveAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
