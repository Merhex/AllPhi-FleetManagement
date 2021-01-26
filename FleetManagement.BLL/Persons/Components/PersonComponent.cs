using FleetManagement.BLL.Persons.Components.Interfaces;
using FleetManagement.BLL.Persons.Contracts;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.Components
{
    public class PersonComponent : IPersonComponent
    {
        private readonly PersonValidator _personValidator;
        private readonly IPersonRepository _personRepository;

        public PersonComponent(
            PersonValidator personValidator,
            IPersonRepository personRepository)
        {
            _personValidator = personValidator;
            _personRepository = personRepository;
        }

        public async Task<IComponentResponse> UpdatePersonInformationAsync(IUpdatePersonInformationContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var person = await GetUniquePerson(contract, response, token);

            UpdatePersonInformation(person, contract);

            await PersonValidation(person, response, token);

            await Persistance(response);

            return response;
        }

        #region PRIVATE
        private async Task<Person> GetUniquePerson(IUpdatePersonInformationContract contract, ComponentResponse response, CancellationToken cancellationToken)
        {
            var person = await _personRepository.FindByNationalNumberAsync(contract.NationalNumber, cancellationToken);
            if (person is null) 
                response.NotFound(contract.NationalNumber);
            else 
                response.Ok();

            return person;
        }

        private static void UpdatePersonInformation(Person person, IUpdatePersonInformationContract contract)
        {
            if (person is null) person = new Person();

            person.AddressLine = contract.AddressLine;
            person.City = contract.City;
            person.DateOfBirth = contract.DateOfBirth;
            person.FirstName = contract.FirstName;
            person.LastName = contract.LastName;
            person.NationalNumber = contract.NationalNumber;
            person.ZipCode = contract.ZipCode;
        }

        private async Task PersonValidation(Person person, ComponentResponse response, CancellationToken cancellationToken) 
        {
            var validation = await _personValidator.ValidateAsync(person, cancellationToken);
            if (validation.IsValid is not true)
                response.ValidationFailure(validation);
        }

        private async Task Persistance(ComponentResponse response)
        {
            if (response.Valid is not true) return;

            var saved = await _personRepository.SaveAsync();
            if (saved is not true) response.PersistanceFailure();
        }
        #endregion
    }
}
