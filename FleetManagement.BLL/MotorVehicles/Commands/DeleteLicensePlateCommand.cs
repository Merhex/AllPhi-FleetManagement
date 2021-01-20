using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record DeleteLicensePlateCommand : IRequest<ICommandResponse>
    {
        public int LicensePlateId { get; set; }
    }
}
