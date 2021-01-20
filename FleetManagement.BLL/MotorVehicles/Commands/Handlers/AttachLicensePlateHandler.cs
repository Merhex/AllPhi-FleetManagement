using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class AttachLicensePlateHandler : IRequestHandler<AttachLicensePlateCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public AttachLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(AttachLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.AttachLicensePlateToMotorVehicleAsync(command, cancellationToken);
        }
    }
}
