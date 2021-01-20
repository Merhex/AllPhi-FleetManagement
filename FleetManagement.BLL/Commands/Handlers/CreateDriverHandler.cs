using FleetManagement.BLL.Commands.Response;
using FleetManagement.BLL.Components.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Commands.Handlers
{
    public class CreateDriverHandler : IRequestHandler<CreateDriverCommand, ICommandResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public CreateDriverHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<ICommandResponse> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.CreateDriverAsync(command, cancellationToken);
        }
    }
}
