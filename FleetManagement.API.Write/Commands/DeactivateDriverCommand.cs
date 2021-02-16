using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record DeactivateDriverCommand : IRequest<IComponentResponse>, IDeactivateDriverContract
    {
        public string NationalNumber { get; init; }
    }
}
