using FleetManagement.BLL.Drivers.BusinessRules;
using FleetManagement.DAL.NHibernate;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL.Drivers.Contracts.Requirements
{
    public class DeactivateDriverRequirments : IBusinessRequirements<IDeactivateDriverContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public DeactivateDriverRequirments(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(IDeactivateDriverContract contract)
        {
            BusinessRules.Add(
                new DriverExists(_serviceProvider.GetRequiredService<IDriverSession>(), contract.NationalNumber));
        }
    }
}
