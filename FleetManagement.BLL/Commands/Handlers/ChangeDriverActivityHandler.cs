﻿using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.Mappings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Commands.Handlers
{
    public class ChangeDriverActivityHandler : IRequestHandler<ChangeDriverActivityStatusCommand, CommandResponse>
    {
        private readonly IDriverComponent _driverComponent;

        public ChangeDriverActivityHandler(IDriverComponent driverComponent)
        {
            _driverComponent = driverComponent;
        }

        public async Task<CommandResponse> Handle(ChangeDriverActivityStatusCommand command, CancellationToken cancellationToken)
        {
            return await _driverComponent.ChangeDriverActitvityAsync(command, cancellationToken);
        }
    }
}
