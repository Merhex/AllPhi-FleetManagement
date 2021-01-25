namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeMotorVehicleOperationalStatusContract
    {
        public string ChassisNumber { get; init; }
        public bool Operational { get; init; }
    }
}
