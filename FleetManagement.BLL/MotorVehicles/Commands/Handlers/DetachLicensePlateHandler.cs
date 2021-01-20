using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class DetachLicensePlateHandler : IRequestHandler<DetachLicensePlaceCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public DetachLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(DetachLicensePlaceCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.DetachLicensePlateFromMotorVehicleAsync(command, cancellationToken);
        }
    }
}
