using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class MotorVehicleMaintenanceConfiguration : IEntityTypeConfiguration<MotorVehicleMaintenance>
    {
        public void Configure(EntityTypeBuilder<MotorVehicleMaintenance> builder)
        {
            builder
                .Property(vm => vm.DescriptionOfWork)
                .HasMaxLength(200);
        }
    }
}
