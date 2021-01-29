﻿using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record CreateLicensePlateCommand : IRequest<IComponentResponse>, ILicensePlateContract
    {
        public string Identifier { get; init; }
    }
}
