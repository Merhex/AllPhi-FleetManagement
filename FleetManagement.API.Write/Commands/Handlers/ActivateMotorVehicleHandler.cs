using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class ActivateMotorVehicleHandler : IRequestHandler<ActivateMotorVehicleCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public ActivateMotorVehicleHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(ActivateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.ActivateMotorVehicleAsync(command, cancellationToken);
        }
    }
}
