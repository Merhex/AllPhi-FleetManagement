using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.Models;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record CreateMotorVehicleCommand : IRequest<IComponentResponse>, ICreateMotorVehicleContract
    {
        public string ChassisNumber { get; init; }
        public string Brand { get; init; }
        public string Model { get; init; }
        public bool Operational { get; init; }
        public int BodyType { get; init; }
        public int PropulsionType { get; init; }
    }
}
