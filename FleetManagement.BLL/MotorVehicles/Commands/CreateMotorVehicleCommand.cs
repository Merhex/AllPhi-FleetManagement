using FleetManagement.BLL.Shared.Interfaces;
using FleetManagement.Models;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record CreateMotorVehicleCommand : IRequest<ICommandResponse>
    {
        public string ChassisNumber { get; init; }
        public string Brand { get; init; }
        public string Model { get; init; }
        public bool Operational { get; init; }
        public MotorVehicleBodyType BodyType { get; init; }
        public MotorVehiclePropulsionType PropulsionType { get; init; }
    }
}
