using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Models
{
    public class MotorVehicle
    {
        public int Id { get; set; }

        [RegularExpression("[A-HJ-NPR-Z0-9]{17}")]
        [Required]
        public string ChassisNumber { get; set; }

        [MaxLength(50)]
        [Required]
        public string Brand { get; set; }

        [MaxLength(50)]
        [Required]
        public string Model { get; set; }

        [Required]
        public bool Operational { get; set; }

        [Required]
        public MotorVehicleBodyType BodyType { get; set; }

        [Required]
        public MotorVehiclePropulsionType PropulsionType { get; set; }

        public Driver Driver { get; set; }
        public int? DriverId { get; set; }

        [Required]
        public ICollection<LicensePlate> LicensePlates { get; set; }

        [Required]
        public ICollection<MotorVehicleMileageSnapshot> MileageHistory { get; set; }

        [Required]
        public ICollection<MotorVehicleWorkOrder> Condition { get; set; }
    }
}
