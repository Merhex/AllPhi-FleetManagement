namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IChangeLicensePlateInUseStatusContract
    {
        public string Identifier { get; set; }
        public bool Status { get; set; }
    }
}
