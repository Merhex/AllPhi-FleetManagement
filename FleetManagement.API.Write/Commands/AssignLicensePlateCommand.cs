using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record AssignLicensePlateCommand : IRequest<IComponentResponse>, IAssignLicensePlateContract
    {
        public int MotorVehicleId { get; init; }
        public int LicensePlateId { get; set; }
    }
}
