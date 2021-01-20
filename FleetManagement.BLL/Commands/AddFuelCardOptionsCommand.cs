using FleetManagement.BLL.Commands.Response;
using MediatR;
using System.Collections.Generic;

namespace FleetManagement.BLL.Commands
{
    public record AddFuelCardOptionsCommand : IRequest<ICommandResponse>
    {
        public int FuelCardId { get; set; }
        public IEnumerable<string> Options { get; init; }
    }
}
