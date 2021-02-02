using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record AssignLicensePlateCommand : IRequest<IComponentResponse>, IAssignLicensePlateContract
    {
        public string ChassisNumber { get; init; }
        public string Identifier { get; init; }
    }
}
