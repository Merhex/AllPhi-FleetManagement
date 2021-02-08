namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IUpdateMotorVehicleContract : IContract
    {
        public string ChassisNumber { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public bool Operational { get; init; }

        public int BodyType { get; init; }

        public int PropulsionType { get; init; }
    }
}
