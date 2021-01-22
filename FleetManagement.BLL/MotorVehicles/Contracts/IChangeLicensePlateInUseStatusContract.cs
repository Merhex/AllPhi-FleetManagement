namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeLicensePlateInUseStatusContract
    {
        public int LicensePlateId { get; set; }
        public bool InUse { get; set; }
    }
}
