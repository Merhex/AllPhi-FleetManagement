using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class ActivateLicensePlateHandler : IRequestHandler<ActivateLicensePlateCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public ActivateLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(ActivateLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.ActivateLicensePlateAsync(command, cancellationToken);
        }
    }
}
