namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IDeactivateMotorVehicleContract : IContract
    {
        public string ChassisNumber { get; init; }
    }
}
