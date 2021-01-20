﻿using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Commands.Handlers
{
    public class CreateMotorVehicleHandler : IRequestHandler<CreateMotorVehicleCommand, ICommandResponse>
    {
        private readonly IMotorVehicleComponent _motorVehicleComponent;

        public CreateMotorVehicleHandler(IMotorVehicleComponent motorVehicleComponent)
        {
            _motorVehicleComponent = motorVehicleComponent;
        }

        public async Task<ICommandResponse> Handle(CreateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            return await _motorVehicleComponent.CreateMotorVehicleAsync(command, cancellationToken);
        }
    }
}
