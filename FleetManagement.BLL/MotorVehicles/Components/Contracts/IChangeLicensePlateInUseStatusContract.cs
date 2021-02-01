namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeLicensePlateInUseStatusContract : IContract
    {
        public string Identifier { get; init; }
        public bool Status { get; init; }
    }
}
