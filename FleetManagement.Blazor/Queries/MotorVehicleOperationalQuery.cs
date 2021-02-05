namespace FleetManagement.Blazor.Queries
{
    public class MotorVehicleOperationalQuery : IQuery
    {
        public string Endpoint { get; }

        public MotorVehicleOperationalQuery(int page, int pageSize)
        {
            Endpoint = $"MotorVehicles?Page={page}&PageSize={pageSize}";
        }
    }
}
