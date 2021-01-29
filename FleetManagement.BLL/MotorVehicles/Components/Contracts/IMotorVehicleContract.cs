namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IMotorVehicleContract : IContract
    {
        public string ChassisNumber { get; init; }
    }
}
