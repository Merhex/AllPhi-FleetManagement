using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ChangeMotorVehicleOperationalStatusCommand : IRequest<IComponentResponse>, IChangeMotorVehicleOperationalStatusContract
    {
        public string ChassisNumber { get; init; }
        public bool Operational { get; init; }
    }
}
