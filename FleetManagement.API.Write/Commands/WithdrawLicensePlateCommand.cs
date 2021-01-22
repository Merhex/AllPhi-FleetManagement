﻿using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record WithdrawLicensePlateCommand : IRequest<IComponentResponse>, IWithdrawLicensePlateContract
    {
        public int LicensePlateId { get; set; }
    }
}
