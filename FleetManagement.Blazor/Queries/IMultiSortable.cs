using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.Blazor.Queries
{
    public interface IMultiSortable
    {
        ICollection<ISortable> Sortables { get; }

        public string GetSortQueryString()
        {
            if (Sortables.Count is 0) return "";

            var sortList = new List<string>();
            foreach (var sortable in Sortables)
            {
                var direction = sortable.Descending ? " desc" : null;

                sortList.Add($"{sortable.PropertyName}{direction}");
            }

            return "sortBy=" + string.Join(',', sortList);
        }
    }
}
