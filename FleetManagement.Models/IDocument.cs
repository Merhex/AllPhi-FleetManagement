using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public interface IDocument
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
