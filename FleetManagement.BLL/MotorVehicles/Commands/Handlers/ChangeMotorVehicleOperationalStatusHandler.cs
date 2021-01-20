using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class ChangeMotorVehicleOperationalStatusHandler : IRequestHandler<ChangeMotorVehicleOperationalStatusCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public ChangeMotorVehicleOperationalStatusHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(ChangeMotorVehicleOperationalStatusCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.ChangeMotorVehicleOperationalStatusAsync(command, cancellationToken);
        }
    }
}
