namespace FleetManagement.Blazor.Queries
{
    public class MotorVehicleDetailedQuery : IQuery
    {
        public string Endpoint { get; }

        public MotorVehicleDetailedQuery(string chassisNumber)
        {
            Endpoint = $"MotorVehicles/detailed?ChassisNumber={chassisNumber}";
        }
    }
}
