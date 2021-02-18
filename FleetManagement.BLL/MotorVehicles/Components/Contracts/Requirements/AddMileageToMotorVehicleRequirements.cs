using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class AddMileageToMotorVehicleRequirements : IBusinessRequirements<IAddMileageToMotorVehicleContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public AddMileageToMotorVehicleRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(IAddMileageToMotorVehicleContract contract)
        {
            var motorVehicleRepository = _serviceProvider.GetRequiredService<IMotorVehicleRepository>();
            var motorVehicleSnapshotRepository = _serviceProvider.GetRequiredService<IMotorVehicleMileageSnapshotRepository>();

            BusinessRules.Add(
                new MotorVehicleExists(motorVehicleRepository, contract.ChassisNumber));

            BusinessRules.Add(
                new MotorVehicleMileageDataValidation(motorVehicleSnapshotRepository, contract.ChassisNumber, contract.Mileage, contract.Date));
        }
    }
}
