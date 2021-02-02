﻿using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record DeactivateMotorVehicleCommand : IRequest<IComponentResponse>, IDeactivateMotorVehicle
    {
        public string ChassisNumber { get ; init; }
    }
}
