﻿using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class CreateMotorVehicleRequirements : IBusinessRequirements<ICreateMotorVehicleContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public CreateMotorVehicleRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(ICreateMotorVehicleContract contract)
        {
            var motorVehicle = CreateMotorVehicleFromContract(contract);
            var motorVehicleRepository = _serviceProvider.GetRequiredService<IMotorVehicleRepository>();
            var mileageRepository = _serviceProvider.GetRequiredService<IMotorVehicleMileageSnapshotRepository>();
            var motorVehicleDataValidator = _serviceProvider.GetRequiredService<MotorVehicleDataValidator>();

            BusinessRules.Add(
                new MotorVehicleCannotExist(motorVehicleRepository, contract.ChassisNumber));

            BusinessRules.Add(
                new MotorVehicleDataValidation(motorVehicleDataValidator, motorVehicle));

            BusinessRules.Add(
                new MotorVehicleMileageDataValidation(mileageRepository, contract.ChassisNumber, contract.Mileage, DateTime.Now));
        }

        #region PRIVATE
        private static MotorVehicle CreateMotorVehicleFromContract(ICreateMotorVehicleContract contract)
        {
            return new MotorVehicle
            {
                BodyType = (MotorVehicleBodyType)contract.BodyType,
                Brand = contract.Brand,
                ChassisNumber = contract.ChassisNumber,
                Model = contract.Model,
                Operational = contract.Operational,
                PropulsionType = (MotorVehiclePropulsionType)contract.PropulsionType
            };
        }
        #endregion
    }
}
