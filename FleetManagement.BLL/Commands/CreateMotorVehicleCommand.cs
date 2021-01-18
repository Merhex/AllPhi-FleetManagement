using FleetManagement.Models;
using MediatR;

namespace FleetManagement.BLL.Commands
{
    public class CreateMotorVehicleCommand : IRequest
    {
        public string ChassisNumber { get; set; }
        public bool Operational { get; set; }
        public MotorVehicleBodyType BodyType { get; set; }
        public MotorVehiclePropulsionType PropulsionType { get; set; }
    }
}
