using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.FuelCards.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class AddFuelCardOptionsHandler : IRequestHandler<AddFuelCardOptionsCommand, IComponentResponse>
    {
        private readonly IFuelCardComponent _fuelCardComponent;

        public AddFuelCardOptionsHandler(IFuelCardComponent fuelCardComponent)
        {
            _fuelCardComponent = fuelCardComponent;
        }

        public async Task<IComponentResponse> Handle(AddFuelCardOptionsCommand command, CancellationToken cancellationToken)
        {
            return await _fuelCardComponent.AddFuelCardOptionsAsync(command, cancellationToken);
        }
    }
}
