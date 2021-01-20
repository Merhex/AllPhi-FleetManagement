using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.FuelCards.Commands
{
    public record DeleteFuelCardCommand : IRequest<ICommandResponse>
    {
        public int FuelCardId { get; set; }
    }
}
