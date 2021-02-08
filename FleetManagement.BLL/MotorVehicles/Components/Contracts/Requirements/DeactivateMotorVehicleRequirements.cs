using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class DeactivateMotorVehicleRequirements : IBusinessRequirements<IDeactivateMotorVehicleContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public DeactivateMotorVehicleRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(IDeactivateMotorVehicleContract contract)
        {
            BusinessRules.Add(
                new MotorVehicleExists(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.ChassisNumber));
        }
    }
}
