using FleetManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FleetManagement.API.Read.Queries
{
    public interface IFilterableQuery
    {
        IEnumerable<Expression<Func<T, bool>>> GetFilters<T>();
    }
}
