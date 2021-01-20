﻿using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record ChangeMotorVehicleOperationalStatusCommand : IRequest<ICommandResponse>
    {
        public int MotorVehicleId { get; init; }
        public bool Operational { get; init; }
    }
}