using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class WithdrawLicensePlateHandler : IRequestHandler<WithdrawLicensePlateCommand, IComponentResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public WithdrawLicensePlateHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<IComponentResponse> Handle(WithdrawLicensePlateCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.WithdrawLicensePlateFromMotorVehicleAsync(command, cancellationToken);
        }
    }
}
