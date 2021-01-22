using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using FleetManagement.BLL.Persons.Components.Interfaces;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class UpdatePersonInformationHandler : IRequestHandler<UpdatePersonInformationCommand, IComponentResponse>
    {
        private readonly IPersonComponent _personComponent;

        public UpdatePersonInformationHandler(IPersonComponent personComponent)
        {
            _personComponent = personComponent;
        }

        public async Task<IComponentResponse> Handle(UpdatePersonInformationCommand command, CancellationToken cancellationToken)
        {
            return await _personComponent.UpdatePersonInformationAsync(command, cancellationToken);
        }
    }
}
