using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class WithdrawLicensePlateHandler : IRequestHandler<WithdrawLicensePlateCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public WithdrawLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(WithdrawLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.WithdrawLicensePlateFromMotorVehicleAsync(command, cancellationToken);
        }
    }
}
