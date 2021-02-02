namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IDeactivateMotorVehicle : IContract
    {
        public string ChassisNumber { get; init; }
    }
}
