using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class LicensePlate 
    {
        public int Id { get; set; }

        [MaxLength(9)]
        [Required]
        [Key]
        public string Identifier { get; set; }

        [Required]
        public bool InUse { get; set; }

        public ICollection<LicensePlateSnapshot> History { get; set; }
    }
}
