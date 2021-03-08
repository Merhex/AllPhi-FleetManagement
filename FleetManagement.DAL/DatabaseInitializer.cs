﻿using Bogus;
using FleetManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                await context.Database.EnsureDeletedAsync();

                if (await context.Database.EnsureCreatedAsync())
                {
                    if (context.Database.GetPendingMigrations().Any())
                        await context.Database.MigrateAsync();

                    SeedDevelopmentData(context);
                    await context.SaveChangesAsync();
                }
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
                .RuleFor(x => x.NationalNumber, f => f.Random.Replace("##.##.##-###.###"))
                .RuleFor(x => x.DateOfBirth, f => f.Date.Past(18))
                .RuleFor(x => x.Active, f => f.Random.Bool(0.9f))
                .RuleFor(x => x.AddressLine, f => f.Address.StreetAddress(true))
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.ZipCode, f => f.Random.Int(1000, 9999))
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName());

            var licensePlate = new Faker<LicensePlate>()
                .RuleFor(x => x.History, () => new List<LicensePlateSnapshot>())
                .RuleFor(x => x.Identifier, f => f.Random.Replace("#-???-###"))
                .RuleFor(x => x.InUse, f => f.Random.Bool(0.1f));

            var mileageSnapshot = new Faker<MotorVehicleMileageSnapshot>()
                .RuleFor(x => x.SnapshotDate, f => f.Date.Recent(180))
                .RuleFor(x => x.Mileage, f => f.Random.Int(0, 5000));

            var motorVehicle = new Faker<MotorVehicle>()
                .RuleFor(x => x.ChassisNumber, f => f.Vehicle.Vin())
                .RuleFor(x => x.Model, f => f.Vehicle.Model())
                .RuleFor(x => x.Brand, f => f.Vehicle.Manufacturer())
                .RuleFor(x => x.Operational, f => f.Random.Bool(0.9f))
                .RuleFor(x => x.MileageHistory, () => new List<MotorVehicleMileageSnapshot>())
                .RuleFor(x => x.LicensePlates, () => new List<LicensePlate>())
                .RuleFor(x => x.BodyType, f => f.Random.Enum<MotorVehicleBodyType>())
                .RuleFor(x => x.PropulsionType, f => f.Random.Enum<MotorVehiclePropulsionType>());

            var licensePlateHistory = new Faker<LicensePlateSnapshot>()
                .RuleFor(x => x.InUse, f => f.Random.Bool())
                .RuleFor(x => x.SnapshotDate, f => f.Date.Between(f.Date.Past(1), DateTime.Now));


            var licensePlates = licensePlate.Generate(50);
            var motorVehicles = motorVehicle.Generate(50);
            var driverLicenses = driverLicense.Generate(50);
            var drivers = driver.Generate(50);
            var random = new Random();

            foreach (var lp in licensePlates)
            {
                var history = licensePlateHistory.Generate(10);

                foreach (var snapshot in history)
                {
                    snapshot.MotorVehicle = motorVehicles[random.Next(50)];
                    lp.History.Add(snapshot);
                }
            }

            foreach (var mv in motorVehicles)
            {
                var history = mileageSnapshot.Generate(10);
                foreach (var snapshot in history)
                    mv.MileageHistory.Add(snapshot);
            }

            for (int i = 0; i < drivers.Count; i++)
                drivers[i].DriverLicense = driverLicenses[i];

            for (int i = 0; i < drivers.Count; i++)
                drivers[i].MotorVehicle = motorVehicles[i];

            for (int i = 0; i < motorVehicles.Count; i++)
                motorVehicles[i].LicensePlates.Add(licensePlates[i]);

            for (int i = 0; i < motorVehicles.Count; i++)
                motorVehicles[i].Driver = drivers[i];

            context.MotorVehicles.AddRange(motorVehicles);
        }
        #endregion
    }
}
