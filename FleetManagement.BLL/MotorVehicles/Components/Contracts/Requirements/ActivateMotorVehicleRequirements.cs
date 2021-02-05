using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class ActivateMotorVehicleRequirements : IBusinessRequirements<IActivateMotorVehicle>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public ActivateMotorVehicleRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(IActivateMotorVehicle contract)
        {
            BusinessRules.Add(
                new MotorVehicleExists(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.ChassisNumber));

            BusinessRules.Add(
                new MotorVehicleNotOperational(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.ChassisNumber));
        }
    }
}
