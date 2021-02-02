using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class ActivateLicensePlateRequirements : IBusinessRequirements<IActivateLicensePlateContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public ActivateLicensePlateRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Read(IActivateLicensePlateContract contract)
        {
            BusinessRules.Add(
               new LicensePlateExists(
                   _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateAssigned(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.Identifier));

            BusinessRules.Add(
                new MotorVehicleNotActiveLicensePlate(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateNotActivated(
                    _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));
        }
    }
}
