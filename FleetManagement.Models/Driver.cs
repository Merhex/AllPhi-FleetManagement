using System;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class Driver : Person
    {
        [Required]
        public bool Active { get; set; }

        public int UserId { get; set; }

        [Required]
        public DriverLicense DriverLicense { get; set; }

        public MotorVehicle MotorVehicle { get; set; }

        public FuelCard FuelCard { get; set; }
    }
}
 