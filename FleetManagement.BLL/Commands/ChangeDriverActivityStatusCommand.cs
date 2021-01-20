using FleetManagement.BLL.Commands.Response;
using MediatR;

namespace FleetManagement.BLL.Commands
{
    public record ChangeDriverActivityStatusCommand : IRequest<ICommandResponse>
    {
        public bool Active { get; init; }
        public int DriverId { get; init; }
    }
}
