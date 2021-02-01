namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeMotorVehicleOperationalStatusContract : IContract
    {
        public string ChassisNumber { get; init; }
        public bool Operational { get; init; }
    }
}
