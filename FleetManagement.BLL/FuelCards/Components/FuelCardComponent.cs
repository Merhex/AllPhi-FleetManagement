using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.FuelCards.Contracts;
using FleetManagement.BLL.FuelCards.Validators;
using FleetManagement.BLL.Shared;
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

        public async Task<IComponentResponse> AddFuelCardOptionsAsync(IAddFuelCardOptionsContract contract, CancellationToken cancellationToken)
        {
            var fuelCard = await _fuelCardRepository.FindByIdAsync(contract.FuelCardId, cancellationToken);
            if (fuelCard is null)
                return ComponentResponse.BadRequest("The fuel card with given id does not exist.");

            if (contract.Options.Any() is not true)
                return ComponentResponse.BadRequest("An empty list of options is not valid.");

            var optionListFromCommand = new List<FuelCardOption>();
            foreach (var option in contract.Options)
            {
                var match = await _fuelCardOptions.FindByNameAsync(option, cancellationToken);

                if (match is null) 
                    return ComponentResponse.BadRequest($"The option with name {option} is not valid option.");
                else 
                    optionListFromCommand.Add(match);
            }

            fuelCard.Options = fuelCard.Options
                                .Union(optionListFromCommand)
                                .ToList();

            var saved = await _fuelCardRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Ok();
        }

        public async Task<IComponentResponse> CreateFuelCardAsync(ICreateFuelCardContract contract, CancellationToken cancellationToken)
        {
            var match = await _fuelCardRepository.FindByCardNumberAsync(contract.CardNumber, cancellationToken);
            if (match is not null)
                return ComponentResponse.BadRequest("The fuel card with given card number already exists.");

            var fuelCard = new FuelCard
            {
                AuthenticationType = (FuelCardAuthenticationType) contract.AuthenticationType,
                PropulsionTypes = (MotorVehiclePropulsionType) contract.PropulsionTypes,
                Blocked = false,
                CardNumber = contract.CardNumber,
                ExpiryDate = contract.ExpiryDate,
                Issued = false,
                PinCode = contract.PinCode
            };

            var validation = await _fuelCardValidator.ValidateAsync(fuelCard, cancellationToken);
            if (validation.IsValid is not true)
                return ComponentResponse.BadRequest(validation);

            _fuelCardRepository.Add(fuelCard);

            var saved = await _fuelCardRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.Created();
        }

        public async Task<IComponentResponse> DeleteFuelCardAsync(IDeleteFuelCardContract contract, CancellationToken cancellationToken)
        {
            var fuelCard = await _fuelCardRepository.FindByIdAsync(contract.FuelCardId, cancellationToken);
            if (fuelCard is null)
                return ComponentResponse.BadRequest("The fuel card with given id does not exist.");

             _fuelCardRepository.Remove(fuelCard);

            var saved = await _fuelCardRepository.SaveAsync();
            if (saved is not true)
                return ComponentResponse.BadRequest("Something went wrong saving to the database.");


            return ComponentResponse.NoContent();
        }
    }
}
