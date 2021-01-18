using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class DriverLicense
    {
        public int Id { get; set; }

        [Required]
        public string NameHolderFirstName { get; set; }

        [Required]
        public string NameHolderLastName { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DriverLicenseType Categories { get; set; }
    }
}
 