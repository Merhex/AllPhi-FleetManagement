using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.BLL.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Mappings;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components
{
    public class FuelCardComponent : IFuelCardComponent
    {
        private readonly IFuelCardRepository _fuelCardRepository;
        private readonly FuelCardValidator _fuelCardValidator;

        public FuelCardComponent(
            IFuelCardRepository fuelCardRepository,
            FuelCardValidator fuelCardValidator)
        {
            _fuelCardRepository = fuelCardRepository;
            _fuelCardValidator = fuelCardValidator;
        }

        public async Task<CommandResponse> CreateFuelCardAsync(CreateFuelCardCommand command, CancellationToken token)
        {
            var match = await _fuelCardRepository.FindByCardNumberAsync(command.CardNumber);

            if (match is not null)
                return CommandResponse.BadRequest("The fuel card with given card number already exists.");

            var fuelCard = new FuelCard
            {
                AuthenticationType = command.AuthenticationType,
                Blocked = false,
                CardNumber = command.CardNumber,
                ExpiryDate = command.ExpiryDate,
                Issued = false,
                PinCode = command.PinCode,
                PropulsionTypes = command.PropulsionTypes
            };

            var validation = await _fuelCardValidator.ValidateAsync(fuelCard, token);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _fuelCardRepository.Add(fuelCard);
            var saved = await _fuelCardRepository.SaveAsync();

            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }
    }
}
