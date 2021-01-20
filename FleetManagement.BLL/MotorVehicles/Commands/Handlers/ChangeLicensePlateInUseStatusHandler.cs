using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class ChangeLicensePlateInUseStatusHandler : IRequestHandler<ChangeLicensePlateInUseStatusCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public ChangeLicensePlateInUseStatusHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(ChangeLicensePlateInUseStatusCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.ChangeLicensePlateInUseStatusAsync(command, cancellationToken);
        }
    }
}
