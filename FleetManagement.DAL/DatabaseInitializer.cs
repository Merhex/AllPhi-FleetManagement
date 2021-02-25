using Bogus;
using FleetManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.DAL
{

    public class DatabaseInitializer : IDatabaseSeeder
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DatabaseInitializer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SeedDatabase()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<FleetManagementContext>();

            if (context is not null)
            {
                if (context.Database.GetPendingMigrations().Any())
                    await context.Database.MigrateAsync();
            }

            await context.Database.EnsureDeletedAsync();

            if (await context.Database.EnsureCreatedAsync())
            {
                SeedDevelopmentData(context);
                await context.SaveChangesAsync();
            }
        }

        #region PRIVATE
        private static void SeedDevelopmentData(FleetManagementContext context)
        {
            var driverLicense = new Faker<DriverLicense>()
                .RuleFor(x => x.Identifier, f => f.Finance.CreditCardNumber())
                .RuleFor(x => x.Categories, f => f.Random.Enum<DriverLicenseType>())
                .RuleFor(x => x.ExpiryDate, f => f.Date.Future(3))
                .RuleFor(x => x.NameHolderLastName, f => f.Name.LastName())
                .RuleFor(x => x.NameHolderFirstName, f => f.Name.FirstName());

            var driver = new Faker<Driver>()
                .CustomInstantiator(x => new Driver { NationalNumber = x.Random.Replace("##.##.##-###.###") })
                .RuleFor(x => x.DateOfBirth, f => f.Date.Past(18))
                .RuleFor(x => x.Active, f => f.Random.Bool(0.9f))
                .RuleFor(x => x.AddressLine, f => f.Address.StreetAddress(true))
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.ZipCode, f => f.Random.Int(1000, 9999))
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName());

            var licensePlate = new Faker<LicensePlate>()
                .RuleFor(x => x.Identifier, f => f.Random.Replace("#-???-###"))
                .RuleFor(x => x.InUse, f => f.Random.Bool(0.1f));

            var mileageSnapshot = new Faker<MotorVehicleMileageSnapshot>()
                .RuleFor(x => x.SnapshotDate, f => f.Date.Recent(180))
                .RuleFor(x => x.Mileage, f => f.Random.Int(0, 5000));

            var motorVehicle = new Faker<MotorVehicle>()
                .RuleFor(x => x.ChassisNumber, f => f.Vehicle.Vin())
                .RuleFor(x => x.Model, f => f.Vehicle.Model())
                .RuleFor(x => x.Brand, f => f.Vehicle.Manufacturer())
                .RuleFor(x => x.BodyType, f => f.Random.Enum<MotorVehicleBodyType>())
                .RuleFor(x => x.PropulsionType, f => f.Random.Enum<MotorVehiclePropulsionType>());

            var licensePlateHistory = new Faker<LicensePlateSnapshot>()
                .RuleFor(x => x.InUse, f => f.Random.Bool())
                .RuleFor(x => x.SnapshotDate, f => f.Date.Between(f.Date.Past(1), DateTime.Now));


            licensePlate
                .RuleFor(x => x.History, licensePlateHistory.Generate(10));

            driver
                .RuleFor(x => x.DriverLicense, f => driverLicense.Generate());

            motorVehicle
                .RuleFor(x => x.Driver, driver.Generate())
                .RuleFor(x => x.MileageHistory, mileageSnapshot.Generate(10))
                .RuleFor(x => x.LicensePlates, f => licensePlate.Generate(f.Random.Int(1, 3)));

            context.MotorVehicles.AddRange(motorVehicle.Generate(200));
        }
        #endregion
    }
}
