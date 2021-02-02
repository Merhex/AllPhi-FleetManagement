using FleetManagement.BLL.Drivers.Components;
using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.FuelCards.Components;
using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Components;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.Persons.Components;
using FleetManagement.BLL.Persons.Components.Interfaces;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.BLL.Persons.Validators.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.BLL
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddBusinessHandlers(this IServiceCollection collection)
        {
            collection.AddTransient<IBusinessHandler, BusinessHandler>();

            collection.AddTransient(typeof(IBusinessHandler<>), typeof(BusinessHandler<>));

            return collection;
        }

        public static IServiceCollection AddBusinessComponents(this IServiceCollection collection)
        {
            collection.AddTransient<IPersonComponent, PersonComponent>();

            collection.AddTransient<IMotorVehicleComponent, MotorVehicleComponent>();

            collection.AddTransient<IFuelCardComponent, FuelCardComponent>();

            collection.AddTransient<IDriverComponent, DriverComponent>();

            return collection;
        }

        public static IServiceCollection AddBusinessDataValidators(this IServiceCollection collection)
        {
            collection.AddTransient<IBelgianNationalNumberValidator, BelgiumNationalNumberValidator>();

            return collection;
        }

        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection collection)
        {
            collection.AddBusinessHandlers();

            collection.AddBusinessRequirements();

            collection.AddBusinessDataValidators();

            collection.AddBusinessComponents();

            return collection;
        }
    }
}
