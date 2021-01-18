using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class MotorVehicleAccidentInsuranceClaim 
    {
        public int Id { get; set; }

        [Required]
        public DateTime LossDate { get; set; }

        [Required]
        public MotorVehicle MotorVehicle { get; set; }

        [Required]
        public ICollection<Photo> DamagePhotos { get; set; }

        [Required]
        public InsuranceCompany InsuranceCompany { get; set; }
    }
}
