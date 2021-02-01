using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class AssignLicensePlateToMotorVehicleRequirements : IBusinessRequirements<IAssignLicensePlateContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public AssignLicensePlateToMotorVehicleRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Read(IAssignLicensePlateContract contract)
        {
            BusinessRules.Add(
                new LicensePlateExists(
                    _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));

            BusinessRules.Add(
                new MotorVehicleExists(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.ChassisNumber));

            BusinessRules.Add(
                new LicensePlateNotInUse(
                    _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateAlreadyAssigned(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.Identifier));
        }
    }
}
