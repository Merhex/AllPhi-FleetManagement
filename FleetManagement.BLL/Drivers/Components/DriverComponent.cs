using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.Drivers.Contracts;
using FleetManagement.BLL.Shared;
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

        public async Task<IComponentResponse> ChangeDriverActivityAsync(IChangeDriverActivityStatusContract contract, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.FindByIdAsync(contract.DriverId, cancellationToken);
            if (driver is null)
                return ComponentResponse.BadRequest("The driver with given national number does not exist.");

            driver.Active = contract.Active;

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Ok();
        }

        public async Task<IComponentResponse> CreateDriverAsync(ICreateDriverContract contract, CancellationToken cancellationToken)
        {
            var match = await _driverRepository.FindDriverByNationalNumberAsync(contract.NationalNumber, cancellationToken);
            if (match is not null)
                return ComponentResponse.BadRequest("The driver already exists.");

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

            var validation = await _personValidator.ValidateAsync(driver, cancellationToken);
            if (validation.IsValid is not true)
                return ComponentResponse.BadRequest(validation);

            _driverRepository.Add(driver);

            var saved = await _driverRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Created();
        }
    }
}
