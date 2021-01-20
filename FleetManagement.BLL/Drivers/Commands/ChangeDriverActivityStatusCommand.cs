using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.Drivers.Commands
{
    public record ChangeDriverActivityStatusCommand : IRequest<ICommandResponse>
    {
        public bool Active { get; init; }
        public int DriverId { get; init; }
    }
}
