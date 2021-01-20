using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record ChangeLicensePlateInUseStatusCommand : IRequest<ICommandResponse>
    {
        public int LicensePlateId { get; set; }
        public bool InUse { get; set; }
    }
}
