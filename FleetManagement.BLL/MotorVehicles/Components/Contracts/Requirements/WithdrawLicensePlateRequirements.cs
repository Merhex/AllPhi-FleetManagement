using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class WithdrawLicensePlateRequirements : IBusinessRequirements<IWithdrawLicensePlateContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public WithdrawLicensePlateRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Read(IWithdrawLicensePlateContract contract)
        {
            BusinessRules.Add(
               new LicensePlateExists(
                   _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateAssigned(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateNotActivated(
                    _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));
        }
    }
}
