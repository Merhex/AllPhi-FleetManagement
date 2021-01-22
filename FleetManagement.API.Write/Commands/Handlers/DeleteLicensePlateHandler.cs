using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class DeleteLicensePlateHandler : IRequestHandler<DeleteLicensePlateCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public DeleteLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(DeleteLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.DeleteLicensePlateAsync(command, cancellationToken);
        }
    }
}
