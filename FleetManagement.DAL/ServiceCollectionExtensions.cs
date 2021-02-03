using FleetManagement.DAL.Repositories;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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
            collection.AddTransient<IReadRepository, ReadRepository>();

            return collection;
        }
    }
}
