﻿using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ActivateMotorVehicleCommand : IRequest<IComponentResponse>, IActivateMotorVehicleContract
    {
        public string ChassisNumber { get; init; }
    }
}
