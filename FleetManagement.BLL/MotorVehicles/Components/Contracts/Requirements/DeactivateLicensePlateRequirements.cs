using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class DeactivateLicensePlateRequirements : IBusinessRequirements<IDeactivateLicensePlateContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public DeactivateLicensePlateRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Read(IDeactivateLicensePlateContract contract)
        {
            BusinessRules.Add(
               new LicensePlateExists(
                   _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateAssigned(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.Identifier));

            BusinessRules.Add(
                new MotorVehicleActiveLicensePlate(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateActivated(
                    _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));
        }
    }
}
