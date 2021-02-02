using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class DeactivateMotorVehicleHandler : IRequestHandler<DeactivateMotorVehicleCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public DeactivateMotorVehicleHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(DeactivateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.DeactivateMotorVehicleAsync(command, cancellationToken);
        }
    }
}
