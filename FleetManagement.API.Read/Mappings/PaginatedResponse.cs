using System.Collections.Generic;

namespace FleetManagement.API.Read.Mappings
{
    public class PaginatedResponse<T> : IPaginatedResponse<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
