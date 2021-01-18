using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class PaperDocumentConfiguration : IEntityTypeConfiguration<PaperDocument>
    {
        public void Configure(EntityTypeBuilder<PaperDocument> builder)
        {
            builder
                .HasOne(d => d.Binaries)
                .WithOne(b => b.PaperDocument)
                .HasForeignKey<PaperDocumentBinaries>(b => b.PaperDocumendId);

            builder
                .Property(d => d.Description)
                .HasMaxLength(200);

            builder
                .Property(d => d.Name)
                .HasMaxLength(50);
        }
    }
}
