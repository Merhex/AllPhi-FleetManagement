using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.Drivers.Contracts;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.Components
{
    public class DriverComponent : IDriverComponent
    {
        private readonly IDriverRepository _driverRepository;
        private readonly PersonValidator _personValidator;

        public DriverComponent(
            IDriverRepository driverRepository,
            PersonValidator validator)
        {
            _driverRepository = driverRepository;
            _personValidator = validator;
        }

        public async Task<IComponentResponse> ChangeDriverActivityAsync(IChangeDriverActivityStatusContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var driver = await GetUniqueDriver(contract.NationalNumber, response, token);

            ChangeDriverActivity(driver, contract.Active);

            await Persistance(response);

            return response;
        }

        public async Task<IComponentResponse> CreateDriverAsync(ICreateDriverContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            await DriverMustNotExist(contract, response, token);

            var driver = CreateAndActivateDriver(contract);

            await DriverValidation(driver, response, token);

            await Persistance(driver, response);

            return response;
        }


        #region PRIVATE
        private static void ChangeDriverActivity(Driver driver, bool activity)
        {
            driver.Active = activity;
        }

        private async Task DriverMustNotExist(ICreateDriverContract contract, ComponentResponse response, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.FindDriverByNationalNumberAsync(contract.NationalNumber, cancellationToken);
            if (driver is not null)
                response.BadRequest().WithTitle("The driver with given national number already exists.");
            else response.Ok();
        }

        private async Task<Driver> GetUniqueDriver(string nationalNumber, ComponentResponse response, CancellationToken cancellationToken) 
        {
            var driver = await _driverRepository.FindDriverByNationalNumberAsync(nationalNumber, cancellationToken);
            if (driver is null)
                response.NotFound();
            else
                response.Ok();

            return driver;
        }

        private static Driver CreateAndActivateDriver(ICreateDriverContract contract)
        {
            var driver = new Driver
            {
                Active = true,
                DateOfBirth = contract.DateOfBirth,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                NationalNumber = contract.NationalNumber,
                City = contract.City,
                AddressLine = contract.AddressLine,
                ZipCode = contract.ZipCode
            };

            return driver;
        }

        private async Task DriverValidation(Driver driver, ComponentResponse response, CancellationToken cancellationToken)
        {
            var validation = await _personValidator.ValidateAsync(driver, cancellationToken);
            if (validation.IsValid is not true)
                response.ValidationFailure(validation);
            else response.Ok();
        }

        private async Task Persistance(ComponentResponse response)
        {
            if (response.Valid is not true) return;

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true)
                response.PersistanceFailure();
            else
                response.Ok();
        }

        private async Task Persistance(Driver driver, ComponentResponse response)
        {
            if (driver is null) return;

            _driverRepository.Add(driver);

            await Persistance(response);
        }
        #endregion
    }
}
