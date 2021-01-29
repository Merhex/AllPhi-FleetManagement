namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeMotorVehicleOperationalStatusContract : IMotorVehicleContract
    {
        public bool Operational { get; init; }
    }
}
