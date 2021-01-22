using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class CreateDriverHandler : IRequestHandler<CreateDriverCommand, IComponentResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public CreateDriverHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<IComponentResponse> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.CreateDriverAsync(command, cancellationToken);
        }
    }
}
