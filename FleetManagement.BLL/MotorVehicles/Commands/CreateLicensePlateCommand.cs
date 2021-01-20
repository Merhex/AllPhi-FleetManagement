using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record CreateLicensePlateCommand : IRequest<ICommandResponse>
    {
        public string Identifier { get; set; }
        public bool InUse { get; set; }
    }
}
