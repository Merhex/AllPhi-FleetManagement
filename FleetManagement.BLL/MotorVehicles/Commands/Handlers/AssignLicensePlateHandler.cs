using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class AssignLicensePlateHandler : IRequestHandler<AssignLicensePlateCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public AssignLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(AssignLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.AssignLicensePlateToMotorVehicleAsync(command, cancellationToken);
        }
    }
}
