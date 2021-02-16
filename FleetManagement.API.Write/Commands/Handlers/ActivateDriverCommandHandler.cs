using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class ActivateDriverCommandHandler : IRequestHandler<ActivateDriverCommand, IComponentResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public ActivateDriverCommandHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<IComponentResponse> Handle(ActivateDriverCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.ActivateDriverAsync(command, cancellationToken);
        }
    }
}