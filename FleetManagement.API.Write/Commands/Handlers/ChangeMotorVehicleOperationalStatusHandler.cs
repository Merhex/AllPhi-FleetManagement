using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class ChangeMotorVehicleOperationalStatusHandler : IRequestHandler<ChangeMotorVehicleOperationalStatusCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public ChangeMotorVehicleOperationalStatusHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(ChangeMotorVehicleOperationalStatusCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.ChangeMotorVehicleOperationalStatusAsync(command, cancellationToken);
        }
    }
}
