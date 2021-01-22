using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class ChangeDriverActivityHandler : IRequestHandler<ChangeDriverActivityStatusCommand, IComponentResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public ChangeDriverActivityHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<IComponentResponse> Handle(ChangeDriverActivityStatusCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.ChangeDriverActivityAsync(command, cancellationToken);
        }
    }
}