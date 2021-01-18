using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder
                .Property(p => p.Description)
                .HasMaxLength(200);

            builder
                .Property(p => p.PhotoUrl)
                .HasMaxLength(2048);
        }
    }
}
