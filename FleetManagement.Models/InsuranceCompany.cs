using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class InsuranceCompany 
    {
        public int Id { get; set; }

        [Required]
        public int ReferenceNumber { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Street { get; set; }

        [MaxLength(50)]
        [Required]
        public string City { get; set; }

        [Range(0, 9999)]
        [Required]
        public int ZipCode { get; set; }
    }
}
