﻿using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record DeleteLicensePlateCommand : IRequest<IComponentResponse>, IDeleteLicensePlateContract
    {
        public string Identifier { get; init; }
    }
}
