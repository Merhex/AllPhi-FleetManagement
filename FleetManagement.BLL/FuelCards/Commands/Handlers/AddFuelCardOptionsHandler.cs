using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.FuelCards.Commands.Handlers
{
    public class AddFuelCardOptionsHandler : IRequestHandler<AddFuelCardOptionsCommand, ICommandResponse>
    {
        private readonly IFuelCardComponent _fuelCardComponent;

        public AddFuelCardOptionsHandler(IFuelCardComponent fuelCardComponent)
        {
            _fuelCardComponent = fuelCardComponent;
        }

        public async Task<ICommandResponse> Handle(AddFuelCardOptionsCommand command, CancellationToken cancellationToken)
        {
            return await _fuelCardComponent.AddFuelCardOptionsAsync(command, cancellationToken);
        }
    }
}
