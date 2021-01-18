using System;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class MotorVehicleMileageSnapshot 
    {
        public int Id { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public DateTime SnapshotDate { get; set; }

        [Required]
        public MotorVehicle MotorVehicle { get; set; }
    }
}
