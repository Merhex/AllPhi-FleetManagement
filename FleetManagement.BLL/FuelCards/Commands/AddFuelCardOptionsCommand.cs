using FleetManagement.BLL.Shared.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace FleetManagement.BLL.FuelCards.Commands
{
    public record AddFuelCardOptionsCommand : IRequest<ICommandResponse>
    {
        public int FuelCardId { get; set; }
        public IEnumerable<string> Options { get; init; }
    }
}
