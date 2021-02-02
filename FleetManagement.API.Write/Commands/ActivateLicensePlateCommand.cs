using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ActivateLicensePlateCommand : IRequest<IComponentResponse>, IActivateLicensePlateContract
    {
        public string Identifier { get; init; }
    }
}
