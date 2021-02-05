using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class InsuranceCompanyConfiguration : IEntityTypeConfiguration<InsuranceCompany>
    {
        public void Configure(EntityTypeBuilder<InsuranceCompany> builder)
        {
            builder
                .Property(g => g.Name)
                .HasMaxLength(50);

            builder
                .Property(g => g.City)
                .HasMaxLength(50);

            builder
                .Property(g => g.AddressLine)
                .HasMaxLength(100);
        }
    }
}
