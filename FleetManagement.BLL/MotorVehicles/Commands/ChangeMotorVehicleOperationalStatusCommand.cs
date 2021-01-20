using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record ChangeMotorVehicleOperationalStatusCommand : IRequest<ICommandResponse>
    {
        public int MotorVehicleId { get; set; }
        public bool Operational { get; set; }
    }
}
