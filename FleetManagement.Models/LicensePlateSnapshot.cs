using System;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class LicensePlateSnapshot 
    {
        public int Id { get; set; }

        [Required]
        public bool InUse { get; set; }

        [Required]
        public DateTime SnapshotDate { get; set; }

        [Required]
        public MotorVehicle MotorVehicle { get; set; }

        [Required]
        public LicensePlate LicensePlate { get; set; }
    }
}
