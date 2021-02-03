using System;
using System.Linq.Expressions;

namespace FleetManagement.API.Read.Queries
{
    public interface IFilter<T>
    {
        bool Satisfy(Expression<Func<T, bool>> predicate);
    }
}
