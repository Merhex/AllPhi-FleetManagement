using FleetManagement.BLL;
using FleetManagement.BLL.MotorVehicles.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record CreateLicensePlateCommand : IRequest<IBusinessRuleHandlerResponse>, ICreateLicensePlateContract
    {
        public string Identifier { get; init; }
    }
}
