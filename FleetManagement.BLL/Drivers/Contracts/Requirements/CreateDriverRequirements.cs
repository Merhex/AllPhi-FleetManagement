using FleetManagement.BLL.Drivers.BusinessRules;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.DAL.NHibernate;
using FleetManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.Contracts.Requirements
{
    public class CreateDriverRequirements : IBusinessRequirements<ICreateDriverContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public CreateDriverRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(ICreateDriverContract contract)
        {
            var driver = CreateDriver(contract);

            BusinessRules.Add(
                new DriverCannotExist(_serviceProvider.GetRequiredService<IDriverSession>(), contract.NationalNumber)); ;

            BusinessRules.Add(
                new DriverDataValidation(_serviceProvider.GetRequiredService<PersonValidator>(), driver));
        }

        private static Driver CreateDriver(ICreateDriverContract contract)
        {
            return new Driver
            {
                Active = true,
                AddressLine = contract.AddressLine,
                City = contract.City,
                DateOfBirth = contract.DateOfBirth,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                NationalNumber = contract.NationalNumber,
                ZipCode = contract.ZipCode
            };
        }
    }
}
