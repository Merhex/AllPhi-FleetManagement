using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record UpdateMotorVehicleCommand : IRequest<IComponentResponse>, IUpdateMotorVehicleContract
    {
        public string ChassisNumber { get; init; }
        public string Brand { get; init; }
        public string Model { get; init; }
        public bool Operational { get; init; }
        public int BodyType { get; init; }
        public int PropulsionType { get; init; }
    }
}
