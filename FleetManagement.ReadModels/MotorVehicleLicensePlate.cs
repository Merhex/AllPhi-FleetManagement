using FleetManagement.Models;
using System.Collections.Generic;

namespace FleetManagement.ReadModels
{
    public class MotorVehicleLicensePlate
    {
        public string ChassisNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Operational { get; set; }
        public int CurrentMileage { get; set; }
        public MotorVehicleBodyType BodyType { get; set; }
        public MotorVehiclePropulsionType PropulsionType { get; set; }
        public LicensePlate LicensePlate { get; set; }
    }
}
