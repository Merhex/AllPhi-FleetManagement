using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ChangeDriverActivityStatusCommand : IRequest<IComponentResponse>, IChangeDriverActivityStatusContract
    {
        public bool Active { get; init; }
        public int DriverId { get; init; }
    }
}
