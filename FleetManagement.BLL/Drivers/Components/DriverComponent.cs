﻿using FleetManagement.BLL.Drivers.Commands;
using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.Shared;
using FleetManagement.BLL.Shared.Interfaces;
using FleetManagement.BLL.Shared.Validators;
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

        public async Task<ICommandResponse> ChangeDriverActitvityAsync(ChangeDriverActivityStatusCommand command, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.FindByIdAsync(command.DriverId, cancellationToken);
            if (driver is null) 
                return CommandResponse.BadRequest("The driver with given national number does not exist.");

            driver.Active = command.Active;

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true) 
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }

        public async Task<ICommandResponse> CreateDriverAsync(CreateDriverCommand command, CancellationToken cancellationToken) 
        {
            var match = await _driverRepository.FindDriverByNationalNumberAsync(command.NationalNumber, cancellationToken);
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
                AddressLine = command.AddressLine,
                ZipCode = command.ZipCode
            };

            var validation = await _personValidator.ValidateAsync(driver, cancellationToken);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _driverRepository.Add(driver);

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }

        public async Task<ICommandResponse> UpdateDriverAsync(UpdateDriverInformationCommand command, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.FindByIdAsync(command.DriverId, cancellationToken);
            if (driver is null)
                return CommandResponse.BadRequest("The driver with given id does not exists.");

            driver.DateOfBirth = command.DateOfBirth;
            driver.DriverLicense = command.DriverLicense;
            driver.FirstName = command.FirstName;
            driver.LastName = command.LastName;
            driver.NationalNumber = command.NationalNumber;
            driver.City = command.City;
            driver.AddressLine = command.AddressLine;
            driver.ZipCode = command.ZipCode;

            var validation = await _personValidator.ValidateAsync(driver, cancellationToken);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }
    }
}
