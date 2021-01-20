using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record DetachLicensePlaceCommand : IRequest<ICommandResponse>
    {
        public int LicensePlateId { get; set; }
    }
}
