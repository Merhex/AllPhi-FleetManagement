using FleetManagement.BLL.Shared.Interfaces;
using MediatR;

namespace FleetManagement.BLL.MotorVehicles.Commands
{
    public record WithdrawLicensePlateCommand : IRequest<ICommandResponse>
    {
        public int LicensePlateId { get; set; }
    }
}
