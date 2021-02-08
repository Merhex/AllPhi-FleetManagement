using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder
                .HasOne(driver => driver.MotorVehicle)
                .WithOne(motorVehicle => motorVehicle.Driver)
                .HasForeignKey<MotorVehicle>(motorVehicle => motorVehicle.DriverId)
                .HasPrincipalKey<Driver>(driver => driver.Id);
        }
    }
}
