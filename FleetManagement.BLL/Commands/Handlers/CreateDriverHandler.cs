using FleetManagement.BLL.Components.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Commands.Handlers
{
    public class CreateDriverHandler : IRequestHandler<CreateDriverCommand>
    {
        private readonly IDriverComponent _driverComponent;

        public CreateDriverHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<Unit> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
        {
            await _driverComponent.CreateDriverAsync(command);

            return Unit.Value;
        }            
    }
}
