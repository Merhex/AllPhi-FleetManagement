using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record DeactivateLicensePlateCommand : IRequest<IComponentResponse>, IDeactivateLicensePlateContract
    {
        public string Identifier { get; init; }
    }
}
