using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class FuelCard 
    {
        public int Id { get; set; }


        [MaxLength(20)]
        [Required]
        public string CardNumber { get; set; }

        [Range(0, 999999)]
        [Required]
        public int PinCode { get; set; }

        [Required]
        public bool Blocked { get; set; }

        [Required]
        public bool Issued { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public FuelCardAuthenticationType AuthenticationType { get; set; }

        [Required]
        public MotorVehiclePropulsionType PropulsionTypes { get; set; }


        public ICollection<FuelCardOption> Options { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public ICollection<FuelCardWorkOrder> WorkOrders { get; set; }
    }
}
