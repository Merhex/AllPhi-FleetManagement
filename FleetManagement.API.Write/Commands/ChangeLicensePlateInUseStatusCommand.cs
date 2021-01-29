using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ChangeLicensePlateInUseStatusCommand : IRequest<IComponentResponse>, IChangeLicensePlateInUseStatusContract
    {
        public string Identifier { get; init; }
        public bool Status { get; init; }
    }
}
