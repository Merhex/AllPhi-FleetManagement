using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ChangeLicensePlateInUseStatusCommand : IRequest<IComponentResponse>, IChangeLicensePlateInUseStatusContract
    {
        public int LicensePlateId { get; set; }
        public bool InUse { get; set; }
    }
}
