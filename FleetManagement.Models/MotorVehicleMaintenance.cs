using System;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class MotorVehicleMaintenance 
    {
        public int Id { get; set; }

        public DateTime? ScheduledDate { get; set; }

        [Required]
        public string DescriptionOfWork { get; set; }

        [Required]
        public MotorVehicle MotorVehicle { get; set; }

        public Garage Garage { get; set; }
    }
}
