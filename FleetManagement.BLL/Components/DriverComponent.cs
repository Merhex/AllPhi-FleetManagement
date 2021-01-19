using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.BLL.Validators;
using FleetManagement.BLL.Validators.Interfaces;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Mappings;
using FleetManagement.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components
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

        public async Task<CommandResponse> ChangeDriverActitvityAsync(ChangeDriverActivityStatusCommand command, CancellationToken token)
        {
            var driver = await _driverRepository.FindByIdAsync(command.DriverId);

            if (driver is null) 
                return CommandResponse.BadRequest("The driver with given national number does not exist.");

            driver.Active = command.Active;

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true) 
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }

        public async Task<CommandResponse> CreateDriverAsync(CreateDriverCommand command, CancellationToken token) 
        {
            var match = await _driverRepository.FindDriverByNationalNumberAsync(command.NationalNumber);

            if (match is not null) 
                return CommandResponse.BadRequest("The driver already exists.");
                
            var driver = new Driver
            {
                Active = true,
                DateOfBirth = command.DateOfBirth,
                DriverLicense = command.DriverLicense,
                FirstName = command.FirstName,
                LastName = command.LastName,
                NationalNumber = command.NationalNumber,
                City = command.City,
                Street = command.Street,
                ZipCode = command.ZipCode
            };

            var validation = await _personValidator.ValidateAsync(driver, token);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _driverRepository.Add(driver);
            var saved = await _driverRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }

        public async Task<CommandResponse> UpdateDriverAsync(UpdateDriverInformationCommand command, CancellationToken token)
        {
            var driver = await _driverRepository.FindByIdAsync(command.DriverId);

            if (driver is null)
                return CommandResponse.BadRequest("The driver with given id does not exists.");

            driver.DateOfBirth = command.DateOfBirth;
            driver.DriverLicense = command.DriverLicense;
            driver.FirstName = command.FirstName;
            driver.LastName = command.LastName;
            driver.NationalNumber = command.NationalNumber;
            driver.City = command.City;
            driver.Street = command.Street;
            driver.ZipCode = command.ZipCode;

            var validation = await _personValidator.ValidateAsync(driver, token);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }
    }
}
