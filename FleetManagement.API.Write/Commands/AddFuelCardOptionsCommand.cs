using System.Collections.Generic;
using FleetManagement.BLL;
using FleetManagement.BLL.FuelCards.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record AddFuelCardOptionsCommand : IRequest<IComponentResponse>, IAddFuelCardOptionsContract
    {
        public string CardNumber { get; set; }
        public IEnumerable<string> Options { get; init; }
    }
}
