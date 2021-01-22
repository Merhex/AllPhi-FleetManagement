using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class ChangeLicensePlateInUseStatusHandler : IRequestHandler<ChangeLicensePlateInUseStatusCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public ChangeLicensePlateInUseStatusHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(ChangeLicensePlateInUseStatusCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.ChangeLicensePlateInUseStatusAsync(command, cancellationToken);
        }
    }
}
