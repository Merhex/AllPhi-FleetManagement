using System;
using System.Linq;
using System.Linq.Expressions;

namespace FleetManagement.DAL
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            return queryable
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize);
        }
        public static IQueryable<T> AddFilters<T>(this IQueryable<T> queryable, params Expression<Func<T, bool>>[] filters)
        {
            if (filters.Any())
                foreach (var filter in filters)
                    queryable = queryable.Where(filter);

            return queryable;
        }
    }
}
