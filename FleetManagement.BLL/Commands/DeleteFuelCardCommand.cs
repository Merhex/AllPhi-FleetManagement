using FleetManagement.BLL.Commands.Response;
using MediatR;

namespace FleetManagement.BLL.Commands
{
    public record DeleteFuelCardCommand : IRequest<ICommandResponse>
    {
        public int FuelCardId { get; set; }
    }
}
