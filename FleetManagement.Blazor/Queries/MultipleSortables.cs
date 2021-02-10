using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Queries
{
    public class MultipleSortables : IMultiSortable
    {
        public ICollection<ISortable> Sortables { get; set; } = new List<ISortable>();
    }
}
