namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeMotorVehicleOperationalStatusContract
    {
        public int MotorVehicleId { get; init; }
        public bool Operational { get; init; }
    }
}
