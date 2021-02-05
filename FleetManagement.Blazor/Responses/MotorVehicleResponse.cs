namespace FleetManagement.Blazor.Responses
{
    public class MotorVehicleResponse
    {
        public string ChassisNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Operational { get; set; }
        public int CurrentMileage { get; set; }
        public int BodyType { get; set; }
        public int PropulsionType { get; set; }
        public string LicensePlateIdentifier { get; set; }
    }
}
