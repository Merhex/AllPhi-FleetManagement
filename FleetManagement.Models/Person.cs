using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Models
{
    
    public class Person
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression("^[0-9]{2}[.]{1}[0-9]{2}[.]{1}[0-9]{2}[-]{1}[0-9]{3}[.]{1}[0-9]{2}$")]
        [Required]
        [Key]
        public string NationalNumber { get; set; }

        [MaxLength(100)]
        [Required]
        public string AddressLine { get; set; }

        [MaxLength(50)]
        [Required]
        public string City { get; set; }

        [Range(1000, 9999)]
        [Required]
        public int ZipCode { get; set; }

    }
}
