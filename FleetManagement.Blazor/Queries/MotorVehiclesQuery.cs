using FleetManagement.Blazor.Filters;

namespace FleetManagement.Blazor.Queries
{
    public class MotorVehiclesQuery : IQuery, IPageable, ISortable
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public MotorVehicleFilter MotorVehicleFilter { get; set; } = new MotorVehicleFilter();
        public string PropertyName { get; set; }
        public bool Descending { get; set; }

        public string Endpoint => $"MotorVehicles?Page={Page}&PageSize={PageSize}{MotorVehicleFilter.GetQueryParamaters()}&PropertyName={PropertyName}&Descending={Descending}";
    }
}
