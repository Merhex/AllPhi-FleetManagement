using FleetManagement.Blazor.Filters;

namespace FleetManagement.Blazor.Queries
{
    public class MotorVehicleOperationalQuery : IQuery
    {
        public string Endpoint { get; }
        public MotorVehicleFilter MotorVehicleFilter { get; set; } = default;

        public string QueryParameters { get; set; }

        public MotorVehicleOperationalQuery(int page, int pageSize, MotorVehicleFilter filter = default)
        {
            Endpoint = $"MotorVehicles?Page={page}&PageSize={pageSize}{filter.GetQueryParamaters()}";
        }
    }
}
