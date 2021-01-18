using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.BLL.Validators;
using FleetManagement.BLL.Validators.Interfaces;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components
{
    public class DriverComponent : IDriverComponent
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IBelgianNationalNumberValidator _belgianNationalNumberValidator;

        public DriverComponent(
            IDriverRepository driverRepository, 
            IBelgianNationalNumberValidator belgianNationalNumberValidator)
        {
            _driverRepository = driverRepository;
            _belgianNationalNumberValidator = belgianNationalNumberValidator;
        }

        public async Task CreateDriverAsync(CreateDriverCommand command) 
        {
            var match = await _driverRepository.FindDriverByNationalNumber(command.NationalNumber);

            if (match is not null) return;

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

            _driverRepository.Add(driver);
            await _driverRepository.SaveAsync();
        }
    }
}
