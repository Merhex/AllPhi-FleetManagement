using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class DeactivateLicensePlateHandler : IRequestHandler<DeactivateLicensePlateCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public DeactivateLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(DeactivateLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.DeactivateLicensePlateAsync(command, cancellationToken);
        }
    }
}
