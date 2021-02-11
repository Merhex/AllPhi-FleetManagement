using FleetManagement.Blazor.Filters;
using System.Collections.Generic;

namespace FleetManagement.Blazor.Queries
{
    public class LicensePlatesQuery : IQuery, IPageable, IMultiSortable
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public LicensePlateFilter LicensePlateFilter { get; set; } = new LicensePlateFilter();
        public ICollection<ISortable> Sortables { get; set; } = new List<ISortable>();

        public string Endpoint
        {
            get
            {
                IPageable pageable = this;
                IMultiSortable multiSortable = this;
                var filters = LicensePlateFilter.GetFilterParameters();

                return $"MotorVehicles/licensePlates?{pageable.GetPaginationQueryString()}&{filters}&{multiSortable.GetSortQueryString()}";
            }
        }
    }
}
