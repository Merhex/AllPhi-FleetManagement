using FleetManagement.BLL.Commands.Response;
using FleetManagement.BLL.Components.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Commands.Handlers
{
    public class UpdateDriverHandler : IRequestHandler<UpdateDriverInformationCommand, ICommandResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public UpdateDriverHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<ICommandResponse> Handle(UpdateDriverInformationCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.UpdateDriverAsync(command, cancellationToken);
        }
    }
}
