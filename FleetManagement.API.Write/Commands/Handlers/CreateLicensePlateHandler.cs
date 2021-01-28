using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class CreateLicensePlateHandler : IRequestHandler<CreateLicensePlateCommand, IBusinessRuleHandlerResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public CreateLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IBusinessRuleHandlerResponse> Handle(CreateLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.CreateLicensePlateAsync(command, cancellationToken);
        }
    }
}
