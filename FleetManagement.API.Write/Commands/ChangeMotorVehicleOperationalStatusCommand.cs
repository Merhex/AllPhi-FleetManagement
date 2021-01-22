using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ChangeMotorVehicleOperationalStatusCommand : IRequest<IComponentResponse>, IChangeMotorVehicleOperationalStatusContract
    {
        public int MotorVehicleId { get; init; }
        public bool Operational { get; init; }
    }
}
