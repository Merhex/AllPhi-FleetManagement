using System;
using System.Linq.Expressions;

namespace FleetManagement.Blazor.Queries
{
    public interface IFilter<T>
    {
        bool Satisfy(Expression<Func<T, bool>> predicate);
    }
}
