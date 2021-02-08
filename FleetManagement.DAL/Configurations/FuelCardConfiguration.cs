using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class FuelCardConfiguration : IEntityTypeConfiguration<FuelCard>
    {
        public void Configure(EntityTypeBuilder<FuelCard> builder)
        {
            builder
                .HasKey(fuelCard => fuelCard.Id);

            builder
                .Property(fuelCard => fuelCard.CardNumber)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
