using FleetManagement.BLL.Commands.Response;
using FleetManagement.BLL.Components.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Commands.Handlers
{
    public class CreateMotorVehicleHandler : IRequestHandler<CreateMotorVehicleCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public CreateMotorVehicleHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(CreateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.CreateMotorVehicle(command, cancellationToken);
        }
    }
}
