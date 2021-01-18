using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class DriverLicenseConfiguration : IEntityTypeConfiguration<DriverLicense>
    {
        public void Configure(EntityTypeBuilder<DriverLicense> builder)
        {
            builder
                .Property(dl => dl.NameHolderFirstName)
                .HasMaxLength(20); 
            builder
                .Property(dl => dl.NameHolderLastName)
                .HasMaxLength(20);

            builder
                .Property(dl => dl.Identifier)
                .HasMaxLength(20);
        }
    }
}
