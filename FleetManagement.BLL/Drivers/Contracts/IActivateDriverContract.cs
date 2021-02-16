namespace FleetManagement.BLL.Drivers.Contracts
{
    public interface IActivateDriverContract : IContract
    {
        public string NationalNumber { get; init; }
    }
}
