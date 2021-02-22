using FleetManagement.Models;
using System.Collections.Generic;

namespace FleetManagement.ReadModels
{
    public class LicensePlateDetailed
    {
        public string Identifier { get; set; }
        public bool InUse { get; set; }
        public MotorVehicle MotorVehicle { get; set; }
        public ICollection<LicensePlateSnapshot> History { get; set; }
    }
}
