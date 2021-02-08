namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IActivateMotorVehicleContract : IContract
    {
        public string ChassisNumber { get; init; }
    }
}
