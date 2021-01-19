using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.Mappings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Commands.Handlers
{
    public class CreateDriverHandler : IRequestHandler<CreateDriverCommand, CommandResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public CreateDriverHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<CommandResponse> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.CreateDriverAsync(command, cancellationToken);
        }
    }
}
