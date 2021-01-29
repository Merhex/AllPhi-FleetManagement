namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeLicensePlateInUseStatusContract : ILicensePlateContract
    {
        public bool Status { get; init; }
    }
}
