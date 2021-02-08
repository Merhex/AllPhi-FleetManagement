using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class UpdateMotorVehicleRequirements : IBusinessRequirements<IUpdateMotorVehicleContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public UpdateMotorVehicleRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(IUpdateMotorVehicleContract contract)
        {
            var motorVehicle = new MotorVehicle
            {
                ChassisNumber = contract.ChassisNumber,
                BodyType = (MotorVehicleBodyType)contract.BodyType,
                PropulsionType = (MotorVehiclePropulsionType)contract.PropulsionType,
                Operational = contract.Operational,
                Model = contract.Model,
                Brand = contract.Brand
            };

            BusinessRules.Add(
                new MotorVehicleExists(
                    _serviceProvider.GetRequiredService<IMotorVehicleRepository>(), contract.ChassisNumber));

            BusinessRules.Add(
                new MotorVehicleDataValidation(
                    _serviceProvider.GetRequiredService<MotorVehicleDataValidator>(), motorVehicle));
        }
    }
}
