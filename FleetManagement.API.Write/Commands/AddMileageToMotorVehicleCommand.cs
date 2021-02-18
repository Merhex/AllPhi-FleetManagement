using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;
using System;

namespace FleetManagement.API.Write.Commands
{
    public record AddMileageToMotorVehicleCommand : IRequest<IComponentResponse>, IAddMileageToMotorVehicleContract
    {
        public string ChassisNumber { get; set; }
        public int Mileage { get; set; }
        public DateTime Date { get; set; }
    }
}
