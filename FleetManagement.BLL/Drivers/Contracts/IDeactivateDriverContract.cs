namespace FleetManagement.BLL.Drivers.Contracts
{
    public interface IDeactivateDriverContract : IContract
    {
        public string NationalNumber { get; init; }
    }
}
