using System;

namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IAddMileageToMotorVehicleContract : IContract
    {
        public string ChassisNumber { get; set; }
        public int Mileage { get; set; }
        public DateTime Date { get; set; }
    }
}
