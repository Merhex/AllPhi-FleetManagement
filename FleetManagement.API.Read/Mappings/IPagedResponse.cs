using System.Collections.Generic;

namespace FleetManagement.API.Read.Mappings
{
    public interface IPaginatedResponse<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
