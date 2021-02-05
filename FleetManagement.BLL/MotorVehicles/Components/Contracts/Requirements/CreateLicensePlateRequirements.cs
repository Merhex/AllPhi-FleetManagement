using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class CreateLicensePlateRequirements : IBusinessRequirements<ICreateLicensePlateContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public CreateLicensePlateRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(ICreateLicensePlateContract contract)
        {
            var licensePlate = new LicensePlate { Identifier = contract.Identifier };

            BusinessRules.Add(
                new LicensePlateCannotExist(
                    _serviceProvider.GetRequiredService<ILicensePlateRepository>(), contract.Identifier));

            BusinessRules.Add(
                new LicensePlateDataValidation(
                    _serviceProvider.GetRequiredService<LicensePlateDataValidator>(), licensePlate));
        }
    }
}
