using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class DeleteLicensePlateHandler : IRequestHandler<DeleteLicensePlateCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public DeleteLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(DeleteLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.DeleteLicensePlateAsync(command, cancellationToken);
        }
    }
}
