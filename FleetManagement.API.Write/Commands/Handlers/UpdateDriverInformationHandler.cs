using System.Threading;
using System.Threading.Tasks;
using FleetManagement.BLL;
using MediatR;

namespace FleetManagement.API.Write.Commands.Handlers
{
    public class UpdateDriverInformationHandler : IRequestHandler<UpdateDriverInformationCommand, IComponentResponse>
    {
        private readonly IRequestHandler<UpdatePersonInformationCommand, IComponentResponse> _personUpdateHandler;

        public UpdateDriverInformationHandler(IRequestHandler<UpdatePersonInformationCommand, IComponentResponse> requestHandler)
        {
            _personUpdateHandler = requestHandler;
        }

        public async Task<IComponentResponse> Handle(UpdateDriverInformationCommand command, CancellationToken cancellationToken)
        {
            var updateCommand = new UpdatePersonInformationCommand
            {
                AddressLine = command.AddressLine,
                City = command.City,
                DateOfBirth = command.DateOfBirth,
                FirstName = command.FirstName,
                LastName = command.LastName,
                NationalNumber = command.NationalNumber,
                ZipCode = command.ZipCode
            };

            return await _personUpdateHandler.Handle(updateCommand, cancellationToken);
        }
    }
}
