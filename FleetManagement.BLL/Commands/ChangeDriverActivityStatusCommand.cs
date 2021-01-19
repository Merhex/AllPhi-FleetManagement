using FleetManagement.Mappings;
using MediatR;

namespace FleetManagement.BLL.Commands
{
    public record ChangeDriverActivityStatusCommand : IRequest<CommandResponse>
    {
        public bool Active { get; init; }
        public int DriverId { get; init; }
    }
}
