using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class MotorVehicleConfiguration : IEntityTypeConfiguration<MotorVehicle>
    {
        public void Configure(EntityTypeBuilder<MotorVehicle> builder)
        {
            builder
                .HasKey(motoVehicle => motoVehicle.Id);

            builder
                .HasMany(x => x.LicensePlates)
                .WithOne(x => x.MotorVehicle)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(v => v.ChassisNumber)
                .HasMaxLength(17)
                .IsRequired()
                .IsFixedLength();
        }
    }
}
