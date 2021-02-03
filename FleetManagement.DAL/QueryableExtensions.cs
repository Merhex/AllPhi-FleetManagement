using System.Linq;

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
    }
}
