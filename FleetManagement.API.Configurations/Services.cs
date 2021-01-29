using FleetManagement.BLL.Drivers.Components;
using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.FuelCards.Components;
using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Components;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Persons.Components;
using FleetManagement.BLL.Persons.Components.Interfaces;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.BLL.Persons.Validators.Interfaces;
using FleetManagement.DAL.Repositories;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.API.Configurations
{
    public static class Services
    {
        public static IServiceCollection AddRequiredDependenciesInContainer(this IServiceCollection services)
        {
            services.AddTransient<IDriverComponent, DriverComponent>();
            services.AddTransient<IMotorVehicleComponent, MotorVehicleComponent>();
            services.AddTransient<IFuelCardComponent, FuelCardComponent>();
            services.AddTransient<IPersonComponent, PersonComponent>();


            return services;
        }
    }
}
