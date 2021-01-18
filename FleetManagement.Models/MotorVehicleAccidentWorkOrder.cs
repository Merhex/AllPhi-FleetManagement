namespace FleetManagement.Models
{
    public class MotorVehicleAccidentWorkOrder : MotorVehicleWorkOrder
    {
        public MotorVehicleAccidentInsuranceClaim Accident { get; set; }

        public MotorVehicleAccidentWorkOrder() : this(new MotorVehicle()) { }

        public MotorVehicleAccidentWorkOrder(MotorVehicle subject) 
            : base(subject)
        {
            Accident = new MotorVehicleAccidentInsuranceClaim();
        }
    }
}
