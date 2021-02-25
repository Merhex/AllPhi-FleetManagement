using Bogus;
using FleetManagement.DAL.Repositories;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddTransient<IDriverRepository, DriverRepository>();

            collection.AddTransient<ILicensePlateRepository, LicensePlateRepository>();

            collection.AddTransient<IMotorVehicleRepository, MotorVehicleRepository>();

            collection.AddTransient<IFuelCardRepository, FuelCardRepository>();

            collection.AddTransient<IFuelCardOptionRepository, FuelCardOptionRepository>();

            collection.AddTransient<ILicensePlateSnapshotRepository, LicensePlateSnapshotRepository>();

            collection.AddTransient<IMotorVehicleMileageSnapshotRepository, MotorVehicleMileageSnapshotRepository>();

            collection.AddTransient<IPersonRepository, PersonRepository>();

            return collection;
        }

        public static IServiceCollection AddReadRepositories(this IServiceCollection collection)
        {
            collection.AddTransient<IReadMotorVehicleRepository, ReadMotorVehicleRepository>();

            collection.AddTransient<IReadLicensePlatesRepository, ReadLicensePlateRepository>();

            return collection;
        }

        public static async Task SeedDatabase(this IApplicationBuilder applicationBuilder)
        {

        } 
    }
}
