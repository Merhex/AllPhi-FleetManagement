using FleetManagement.Blazor.Filters;
using System.Collections.Generic;

namespace FleetManagement.Blazor.Queries
{
    public class DriversQuery : IQuery, IPageable, IMultiSortable
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public ICollection<ISortable> Sortables { get; set; } = new List<ISortable>();
        public DriverFilter DriverFilter { get; set; } = new DriverFilter();

        public string Endpoint
        {
            get
            {
                IPageable pageable = this;
                IMultiSortable multiSortable = this;
                var filters = DriverFilter.GetFilterParameters();

                return $"Drivers?{pageable.GetPaginationQueryString()}&{filters}&{multiSortable.GetSortQueryString()}";
            }
        }
    }
}
