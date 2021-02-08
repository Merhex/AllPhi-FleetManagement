using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class UpdateMotorVehicleHandler : IRequestHandler<UpdateMotorVehicleCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public UpdateMotorVehicleHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(UpdateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.UpdateMotorVehicleAsync(command, cancellationToken);
        }
    }
}
