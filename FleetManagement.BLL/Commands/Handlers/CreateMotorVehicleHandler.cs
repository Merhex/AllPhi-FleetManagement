using FleetManagement.BLL.Components.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Commands.Handlers
{
    public class CreateMotorVehicleHandler : IRequestHandler<CreateMotorVehicleCommand>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public CreateMotorVehicleHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<Unit> Handle(CreateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            await _motorVehicleComponent.CreateMotorVehicle(command);

            return Unit.Value;
        }
    }
}
