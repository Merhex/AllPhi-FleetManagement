using System.Collections.Generic;

namespace FleetManagement.API.Read.Mappings
{
    public interface IPaginatedResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
