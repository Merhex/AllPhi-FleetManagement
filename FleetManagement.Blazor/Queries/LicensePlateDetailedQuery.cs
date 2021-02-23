using FleetManagement.Blazor.Filters;
using System.Collections.Generic;

namespace FleetManagement.Blazor.Queries
{
    public class LicensePlateDetailedQuery : IQuery, IPageable, IMultiSortable
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public LicensePlateHistoryFilter LicensePlateHistoryFilter { get; set; } = new LicensePlateHistoryFilter();
        public ICollection<ISortable> Sortables { get; set; } = new List<ISortable>();

        public string Endpoint
        {
            get
            {
                IPageable pageable = this;
                IMultiSortable multiSortable = this;
                var filterParams = LicensePlateHistoryFilter.GetFilterParameters();

                return $"MotorVehicles/licensePlate/details?{pageable.GetPaginationQueryString()}&{filterParams}&{multiSortable.GetSortQueryString()}";
            }
        }
    }
}
