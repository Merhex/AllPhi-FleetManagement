using System.Collections.Generic;

namespace FleetManagement.WinForms.Responses
{
    public class PaginatedResponse<T> : IPaginatedResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
