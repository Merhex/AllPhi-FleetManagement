namespace FleetManagement.BLL.Drivers.Contracts
{
    public interface IChangeDriverActivityStatusContract
    {
        public bool Active { get; init; }
        public int DriverId { get; init; }
    }
}
