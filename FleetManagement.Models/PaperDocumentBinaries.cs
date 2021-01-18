using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class PaperDocumentBinaries
    {
        public int Id { get; set; }

        [Required]
        public byte[] Binaries { get; set; }

        [Required]
        public PaperDocument PaperDocument { get; set; }
        public int PaperDocumendId { get; set; }
    }
}
