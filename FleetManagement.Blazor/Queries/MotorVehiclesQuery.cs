using FleetManagement.Blazor.Filters;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Blazor.Queries
{
    public class MotorVehiclesQuery : IQuery, IPageable, IMultiSortable
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public MotorVehicleFilter MotorVehicleFilter { get; set; } = new MotorVehicleFilter();
        public ICollection<ISortable> Sortables { get; set; } = new List<ISortable>();

        public string Endpoint
        {
            get
            {
                IPageable pageable = this;
                IMultiSortable multiSortable = this;
                var filters = MotorVehicleFilter.GetFilterParameters();

                return $"MotorVehicles?{pageable.GetPaginationQueryString()}&{filters}&{multiSortable.GetSortQueryString()}";
            }
        }
    }
}
