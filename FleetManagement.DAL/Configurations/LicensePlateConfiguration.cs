using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class LicensePlateConfiguration : IEntityTypeConfiguration<LicensePlate>
    {
        public void Configure(EntityTypeBuilder<LicensePlate> builder)
        {
            builder
                .HasKey(licensePlate => licensePlate.Id);

            builder
                .Property(l => l.Identifier)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
