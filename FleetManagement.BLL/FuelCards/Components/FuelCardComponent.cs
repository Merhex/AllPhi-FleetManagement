using FleetManagement.BLL.FuelCards.Commands;
using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.FuelCards.Validators;
using FleetManagement.BLL.Shared;
using FleetManagement.BLL.Shared.Interfaces;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.FuelCards.Components
{
    public class FuelCardComponent : IFuelCardComponent
    {
        private readonly IFuelCardRepository _fuelCardRepository;
        private readonly IFuelCardOptionRepository _fuelCardOptions;
        private readonly FuelCardValidator _fuelCardValidator;

        public FuelCardComponent(
            IFuelCardRepository fuelCardRepository,
            IFuelCardOptionRepository fuelCardOptions,
            FuelCardValidator fuelCardValidator)
        {
            _fuelCardRepository = fuelCardRepository;
            _fuelCardOptions = fuelCardOptions;
            _fuelCardValidator = fuelCardValidator;
        }

        public async Task<ICommandResponse> AddFuelCardOptionsAsync(AddFuelCardOptionsCommand command, CancellationToken cancellationToken)
        {
            var fuelCard = await _fuelCardRepository.FindByIdAsync(command.FuelCardId, cancellationToken);
            if (fuelCard is null)
                return CommandResponse.BadRequest("The fuel card with given id does not exist.");

            if (command.Options.Any() is not true)
                return CommandResponse.BadRequest("An empty list of options is not valid.");

            var optionListFromCommand = new List<FuelCardOption>();
            foreach (var option in command.Options)
            {
                var match = await _fuelCardOptions.FindByNameAsync(option, cancellationToken);

                if (match is null) 
                    return CommandResponse.BadRequest($"The option with name {option} is not valid option.");
                else 
                    optionListFromCommand.Add(match);
            }

            fuelCard.Options = fuelCard.Options
                                .Union(optionListFromCommand)
                                .ToList();

            var saved = await _fuelCardRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }

        public async Task<ICommandResponse> CreateFuelCardAsync(CreateFuelCardCommand command, CancellationToken cancellationToken)
        {
            var match = await _fuelCardRepository.FindByCardNumberAsync(command.CardNumber, cancellationToken);
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

            var validation = await _fuelCardValidator.ValidateAsync(fuelCard, cancellationToken);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _fuelCardRepository.Add(fuelCard);

            var saved = await _fuelCardRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }

        public async Task<ICommandResponse> DeleteFuelCardAsync(DeleteFuelCardCommand command, CancellationToken cancellationToken)
        {
            var fuelCard = await _fuelCardRepository.FindByIdAsync(command.FuelCardId, cancellationToken);
            if (fuelCard is null)
                return CommandResponse.BadRequest("The fuel card with given id does not exist.");

             _fuelCardRepository.Remove(fuelCard);

            var saved = await _fuelCardRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.NoContent();
        }
    }
}
