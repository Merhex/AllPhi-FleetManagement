using FleetManagement.BLL;
using FleetManagement.BLL.FuelCards.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record DeleteFuelCardCommand : IRequest<IComponentResponse>, IDeleteFuelCardContract
    {
        public string CardNumber { get; set; }
    }
}
