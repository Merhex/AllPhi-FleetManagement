namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IActivateMotorVehicle : IContract
    {
        public string ChassisNumber { get; init; }
    }
}
