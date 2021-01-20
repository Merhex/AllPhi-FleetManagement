using FleetManagement.BLL.FuelCards.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.FuelCards.Commands.Handlers
{
    public class DeleteFuelCardHandler : IRequestHandler<DeleteFuelCardCommand, ICommandResponse>
    {
        private readonly IFuelCardComponent _fuelCardComponent;

        public DeleteFuelCardHandler(IFuelCardComponent fuelCardComponent)
        {
            _fuelCardComponent = fuelCardComponent;
        }

        public async Task<ICommandResponse> Handle(DeleteFuelCardCommand command, CancellationToken cancellationToken)
        {
            return await _fuelCardComponent.DeleteFuelCardAsync(command, cancellationToken);
        }
    }
}
