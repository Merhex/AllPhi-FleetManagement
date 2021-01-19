﻿using FleetManagement.BLL.Commands;
using FleetManagement.Mappings;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components.Interfaces
{
    public interface IMotorVehicleComponent
    {
        Task<CommandResponse> CreateMotorVehicle(CreateMotorVehicleCommand command, CancellationToken token);
    }
}
