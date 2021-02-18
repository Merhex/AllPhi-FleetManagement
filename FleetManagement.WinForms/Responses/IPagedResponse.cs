using System.Collections.Generic;

namespace FleetManagement.WinForms.Responses
{
    public interface IPaginatedResponse<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
