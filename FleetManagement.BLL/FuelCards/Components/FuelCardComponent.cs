using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.FuelCards.Contracts;
using FleetManagement.BLL.FuelCards.Validators;
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

        public async Task<IComponentResponse> AddFuelCardOptionsAsync(IAddFuelCardOptionsContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            var fuelCard = await GetUniqueFuelCard(contract.CardNumber, response, token);

            var options = await FuelCardOptionsValidation(contract, response, token);

            AddFuelCardOptions(fuelCard, options);

            await Persistance(response);

            return response;
        }

        public async Task<IComponentResponse> CreateFuelCardAsync(ICreateFuelCardContract contract, CancellationToken token)
        {
            var response = new ComponentResponse();

            await CheckIfFuelCardExists(contract.CardNumber, response, token);

            var fuelCard = CreateFuelCard(contract);

            await FuelCardValidation(fuelCard, response, token);

            await Persistance(fuelCard, response);

            return response;
        }

        public async Task<IComponentResponse> DeleteFuelCardAsync(IDeleteFuelCardContract contract, CancellationToken cancellationToken)
        {
            var response = new ComponentResponse();

            var fuelCard = await GetUniqueFuelCard(contract.CardNumber, response, cancellationToken);

            DeleteFuelCard(fuelCard);

            await Persistance(response);

            return response;
        }

        #region PRIVATE
        private void DeleteFuelCard(FuelCard fuelCard)
        {
            if (fuelCard is null) return;

            _fuelCardRepository.Remove(fuelCard);
        }

        private static FuelCard CreateFuelCard(ICreateFuelCardContract contract)
        {
            var fuelCard = new FuelCard
            {
                AuthenticationType = (FuelCardAuthenticationType)contract.AuthenticationType,
                PropulsionTypes = (MotorVehiclePropulsionType)contract.PropulsionTypes,
                Blocked = false,
                CardNumber = contract.CardNumber,
                ExpiryDate = contract.ExpiryDate,
                Issued = false,
                PinCode = contract.PinCode
            };

            return fuelCard;
        }

        private async Task FuelCardValidation(FuelCard fuelCard, ComponentResponse response, CancellationToken token)
        {
            var validation = await _fuelCardValidator.ValidateAsync(fuelCard, token);
            if (validation.IsValid is not true)
                response.ValidationFailure(validation);
            else response.Ok();
        }

        private async Task<FuelCard> GetUniqueFuelCard(string cardNumber, ComponentResponse response, CancellationToken token)
        {
            var fuelCard = await _fuelCardRepository.FindByCardNumberAsync(cardNumber, token);
            if (fuelCard is null)
                response.NotFound();
            else 
                response.Ok();

            return fuelCard;
        }

        private async Task CheckIfFuelCardExists(string cardNumber, ComponentResponse response, CancellationToken token)
        {
            var fuelCard = await _fuelCardRepository.FindByCardNumberAsync(cardNumber, token);
            if (fuelCard is not null)
                response.AlreadyExists();
            else
                response.Ok();
        }

        private async Task<List<FuelCardOption>> FuelCardOptionsValidation(IAddFuelCardOptionsContract contract, ComponentResponse response, CancellationToken token)
        {
            if (contract.Options.Any() is not true) 
                response.BadRequest().AddErrorMessage("There are no fuel card options given in your request.");

            var optionListFromCommand = new List<FuelCardOption>();
            foreach (var option in contract.Options)
            {
                var match = await _fuelCardOptions.FindByNameAsync(option, token);

                if (match is null) 
                    response.AddErrorMessage($"The option with name {option} is not valid option.");
                else
                    optionListFromCommand.Add(match);
            }

            return optionListFromCommand;
        }

        private static void AddFuelCardOptions(FuelCard fuelCard, List<FuelCardOption> options)
        {
            if (fuelCard is null) return;
            if (options.Count is 0) return;

            fuelCard.Options = fuelCard.Options
                    .Union(options)
                    .ToList();
        }

        private async Task Persistance(ComponentResponse response)
        {
            if (response.Valid is not true) return;

            var saved = await _fuelCardRepository.SaveAsync();
            if (saved is not true)
                response.PersistanceFailure();
            else
                response.Ok();
        }

        private async Task Persistance(FuelCard fuelCard, ComponentResponse response)
        {
            if (fuelCard is null) return;

            _fuelCardRepository.Add(fuelCard);

            await Persistance(response);
        }
        #endregion
    }
}
