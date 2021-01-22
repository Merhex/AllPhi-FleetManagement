using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.FuelCards.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class DeleteFuelCardHandler : IRequestHandler<DeleteFuelCardCommand, IComponentResponse>
    {
        private readonly IFuelCardComponent _fuelCardComponent;

        public DeleteFuelCardHandler(IFuelCardComponent fuelCardComponent)
        {
            _fuelCardComponent = fuelCardComponent;
        }

        public async Task<IComponentResponse> Handle(DeleteFuelCardCommand command, CancellationToken cancellationToken)
        {
            return await _fuelCardComponent.DeleteFuelCardAsync(command, cancellationToken);
        }
    }
}
