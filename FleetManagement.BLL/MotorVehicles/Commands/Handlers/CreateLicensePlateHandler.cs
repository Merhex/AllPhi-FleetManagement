using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class CreateLicensePlateHandler : IRequestHandler<CreateLicensePlateCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public CreateLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(CreateLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.CreateLicensePlateAsync(command, cancellationToken);
        }
    }
}
