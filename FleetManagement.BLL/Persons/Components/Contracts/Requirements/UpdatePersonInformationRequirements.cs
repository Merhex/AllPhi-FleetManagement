using FleetManagement.BLL.Persons.BusinessRules;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.DAL.NHibernate;
using FleetManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.Contracts.Requirements
{
    public class UpdatePersonInformationRequirements : IBusinessRequirements<IUpdatePersonInformationContract>
    {
        private readonly IServiceProvider _serviceProvider;

        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public UpdatePersonInformationRequirements(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBusinessRules(IUpdatePersonInformationContract contract)
        {
            var person = CreatePerson(contract);

            BusinessRules.Add(
                new PersonExists(_serviceProvider.GetRequiredService<IPersonSession>(), contract.NationalNumber));

            BusinessRules.Add(
                new PersonDataValidation(_serviceProvider.GetRequiredService<PersonValidator>(), person));
        }

        #region PRIVATE
        private static Person CreatePerson(IUpdatePersonInformationContract contract)
        {
            return new Person
            {
                AddressLine = contract.AddressLine,
                City = contract.City,
                DateOfBirth = contract.DateOfBirth,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                NationalNumber = contract.NationalNumber,
                ZipCode = contract.ZipCode
            };
        }
        #endregion
    }
}
