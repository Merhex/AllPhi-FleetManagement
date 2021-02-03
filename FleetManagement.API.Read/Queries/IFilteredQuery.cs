using System.Collections.Generic;

namespace FleetManagement.API.Read.Queries
{
    public interface IFilteredQuery<T>
    {
        public IEnumerable<IFilter<T>> Filters { get; set; }
    }
}
