using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.FuelCards.Commands.Handlers
{
    public class CreateFuelCardHandler : IRequestHandler<CreateFuelCardCommand, ICommandResponse>
    {
        private readonly IFuelCardComponent _fuelCardComponent;

        public CreateFuelCardHandler(IFuelCardComponent fuelCardComponent)
        {
            _fuelCardComponent = fuelCardComponent;
        }

        public async Task<ICommandResponse> Handle(CreateFuelCardCommand command, CancellationToken cancellationToken)
        {
            return await _fuelCardComponent.CreateFuelCardAsync(command, cancellationToken);
        }
    }
}
