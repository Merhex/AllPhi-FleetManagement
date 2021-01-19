using FleetManagement.BLL.Components;
using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.BLL.Validators;
using FleetManagement.BLL.Validators.Interfaces;
using FleetManagement.DAL.Repositories;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.API.Configurations
{
    public static class Services
    {
        public static IServiceCollection AddRequiredDependenciesInContainer(this IServiceCollection services)
        {
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<IDriverComponent, DriverComponent>();
            services.AddTransient<IMotorVehicleComponent, MotorVehicleComponent>();
            services.AddTransient<IMotorVehicleRepository, MotorVehicleRepository>();
            services.AddTransient<IBelgianNationalNumberValidator, BelgianNationalNumberValidator>();
            services.AddTransient<IFuelCardRepository, FuelCardRepository>();
            services.AddTransient<IFuelCardComponent, FuelCardComponent>();

            return services;
        }
    }
}
