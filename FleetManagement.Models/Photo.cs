using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class Photo : IDocument
    {
        public int Id { get; set; }

        [MaxLength(2048)]
        [Required]
        public string PhotoUrl { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
    }
}
