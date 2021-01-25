namespace FleetManagement.BLL.Drivers.Contracts
{
    public interface IChangeDriverActivityStatusContract
    {
        public bool Active { get; init; }
        public string NationalNumber { get; init; }
    }
}
