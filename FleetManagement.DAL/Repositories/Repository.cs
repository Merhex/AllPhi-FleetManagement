using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Remove(T entity) =>
            _context.Set<T>().Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) =>
            _context.Set<T>().RemoveRange(entities);

        public async Task<bool> SaveAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
