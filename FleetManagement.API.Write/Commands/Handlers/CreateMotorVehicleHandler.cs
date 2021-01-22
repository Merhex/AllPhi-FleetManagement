using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class CreateMotorVehicleHandler : IRequestHandler<CreateMotorVehicleCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public CreateMotorVehicleHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(CreateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.CreateMotorVehicleAsync(command, cancellationToken);
        }
    }
}
