using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : class
    {
        protected readonly FleetManagementContext _context;

        public Repository(FleetManagementContext context)
        {
            _context = context;
        }

        public void Add(T entity) =>
            _context.Set<T>().Add(entity);

        public void AddRange(IEnumerable<T> entities) =>
            _context.Set<T>().AddRange(entities);

        public async Task<T> FindAsync(TKey key) =>
            await _context.Set<T>().FindAsync(key);

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _context.Set<T>().FindAsync(predicate);


        public async Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate) =>
            await _context.Set<T>().Where(predicate).ToListAsync();

        public void Remove(T entity) =>
            _context.Set<T>().Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) =>
            _context.Set<T>().RemoveRange(entities);

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
