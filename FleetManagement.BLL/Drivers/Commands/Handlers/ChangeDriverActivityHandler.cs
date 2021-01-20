using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.Commands.Handlers
{
    public class ChangeDriverActivityHandler : IRequestHandler<ChangeDriverActivityStatusCommand, ICommandResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public ChangeDriverActivityHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<ICommandResponse> Handle(ChangeDriverActivityStatusCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.ChangeDriverActitvityAsync(command, cancellationToken);
        }
    }
}