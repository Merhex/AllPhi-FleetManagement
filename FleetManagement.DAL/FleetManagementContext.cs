using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FleetManagement.DAL
{
    public class FleetManagementContext : DbContext
    {
        public DbSet<PaperDocument> PaperDocuments { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        public DbSet<FuelCard> FuelCards { get; set; }
        public DbSet<FuelCardOption> FuelCardOptions { get; set; }
        public DbSet<FuelCardRequestWorkOrder> FuelCardRequestWorkOrders { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<LicensePlate> LicensePlates { get; set; }
        public DbSet<LicensePlateSnapshot> LicensePlateSnapshots { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<MotorVehicle> MotorVehicles { get; set; }
        public DbSet<MotorVehicleAccidentInsuranceClaim> MotorVehicleAccidentInsuranceClaims { get; set; }
        public DbSet<MotorVehicleAccidentWorkOrder> MotorVehicleAccidentWorkOrders { get; set; }
        public DbSet<MotorVehicleMaintenance> MotorVehicleMaintenances { get; set; }
        public DbSet<MotorVehicleMaintenanceWorkOrder> MotorVehicleMaintenanceWorkOrders { get; set; }
        public DbSet<MotorVehicleMileageSnapshot> MotorVehicleMileageSnapshots { get; set; }


        public FleetManagementContext(DbContextOptions options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(FleetManagementContext).Assembly);
        }
    }
}
