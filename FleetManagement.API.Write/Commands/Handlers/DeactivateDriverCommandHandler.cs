using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class DeactivateDriverCommandHandler : IRequestHandler<DeactivateDriverCommand, IComponentResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public DeactivateDriverCommandHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<IComponentResponse> Handle(DeactivateDriverCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.DeactivateDriverAsync(command, cancellationToken);
        }
    }
}