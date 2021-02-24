using System.Collections.Generic;

namespace FleetManagement.Blazor.States
{
    public class FilterState
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool FilterIsVisible { get; set; }
        public List<ColumnState> ColumnStates { get; set; } = new List<ColumnState>();
    }
}
