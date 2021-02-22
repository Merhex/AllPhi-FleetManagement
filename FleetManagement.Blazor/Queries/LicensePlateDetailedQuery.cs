namespace FleetManagement.Blazor.Queries
{
    public class LicensePlateDetailedQuery : IQuery
    {
        public string Endpoint { get; }

        public LicensePlateDetailedQuery(string identifier)
        {
            Endpoint = $"MotorVehicles/licensePlate/detailed?Identifier={identifier}";
        }
    }
}
