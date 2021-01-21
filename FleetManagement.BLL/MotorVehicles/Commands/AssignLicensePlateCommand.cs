using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record AssignLicensePlateCommand : IRequest<ICommandResponse>
    {
        public int MotorVehicleId { get; init; }
        public int LicensePlateId { get; set; }
    }
}
