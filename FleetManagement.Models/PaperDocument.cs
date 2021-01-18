using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class PaperDocument : IDocument
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Description { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public PaperDocumentBinaries Binaries { get; set; }
    }
}
