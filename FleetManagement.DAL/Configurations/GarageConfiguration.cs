using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
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
