namespace FleetManagement.Models
{
    public abstract class MotorVehicleWorkOrder : WorkOrder<MotorVehicle>
    {
        public MotorVehicleWorkOrder() : this(new MotorVehicle()) { }

        public MotorVehicleWorkOrder(MotorVehicle subject) : base(subject) { }
    }
}
