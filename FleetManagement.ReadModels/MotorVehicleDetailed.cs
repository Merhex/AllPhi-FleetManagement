using FleetManagement.Models;
using System.Collections.Generic;

namespace FleetManagement.ReadModels
{
    public class MotorVehicleDetailed
    {
        public string ChassisNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Operational { get; set; }
        public MotorVehicleBodyType BodyType { get; set; }
        public MotorVehiclePropulsionType PropulsionType { get; set; }
        public Driver Driver { get; set; }
        public ICollection<LicensePlate> LicensePlates { get; set; }
        public ICollection<MotorVehicleMileageSnapshot> MileageHistory { get; set; }
        public ICollection<MotorVehicleWorkOrder> Condition { get; set; }
    }
}
