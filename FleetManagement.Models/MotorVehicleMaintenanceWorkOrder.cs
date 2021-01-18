namespace FleetManagement.Models
{
    public class MotorVehicleMaintenanceWorkOrder : MotorVehicleWorkOrder
    {
        public MotorVehicleMaintenance Maintenance { get; set; }

        public MotorVehicleMaintenanceWorkOrder() : this(new MotorVehicle()) { }

        public MotorVehicleMaintenanceWorkOrder(MotorVehicle subject)
            : base(subject)
        {
            Maintenance = new MotorVehicleMaintenance();
        }
    }
}
