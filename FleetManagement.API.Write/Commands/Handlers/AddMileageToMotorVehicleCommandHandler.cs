using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class AddMileageToMotorVehicleCommandHandler : IRequestHandler<AddMileageToMotorVehicleCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public AddMileageToMotorVehicleCommandHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(AddMileageToMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.AddMileageToMotorVehicleAsync(command, cancellationToken);
        }
    }
}
