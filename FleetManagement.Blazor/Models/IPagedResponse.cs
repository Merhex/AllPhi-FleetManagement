using System.Collections.Generic;

namespace FleetManagement.Blazor.Models
{
    public interface IPaginatedResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
