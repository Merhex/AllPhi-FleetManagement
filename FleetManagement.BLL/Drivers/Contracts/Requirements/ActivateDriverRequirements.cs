using FleetManagement.BLL.Drivers.BusinessRules;
using FleetManagement.DAL.NHibernate;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FleetManagement.BLL.Drivers.Contracts.Requirements
{
    public class ActivateDriverRequirements : IBusinessRequirements<IActivateDriverContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public ActivateDriverRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(IActivateDriverContract contract)
        {
            BusinessRules.Add(
                new DriverExists(_serviceProvider.GetRequiredService<IDriverSession>(), contract.NationalNumber));
        }
    }
}
