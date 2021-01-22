using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class AssignLicensePlateHandler : IRequestHandler<AssignLicensePlateCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public AssignLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(AssignLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.AssignLicensePlateToMotorVehicleAsync(command, cancellationToken);
        }
    }
}
