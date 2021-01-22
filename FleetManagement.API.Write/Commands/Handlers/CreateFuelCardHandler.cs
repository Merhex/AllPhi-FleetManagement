using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.FuelCards.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class CreateFuelCardHandler : IRequestHandler<CreateFuelCardCommand, IComponentResponse>
    {
        private readonly IFuelCardComponent _fuelCardComponent;

        public CreateFuelCardHandler(IFuelCardComponent fuelCardComponent)
        {
            _fuelCardComponent = fuelCardComponent;
        }

        public async Task<IComponentResponse> Handle(CreateFuelCardCommand command, CancellationToken cancellationToken)
        {
            return await _fuelCardComponent.CreateFuelCardAsync(command, cancellationToken);
        }
    }
}
