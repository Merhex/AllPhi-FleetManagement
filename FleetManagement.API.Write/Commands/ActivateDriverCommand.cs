using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record ActivateDriverCommand : IRequest<IComponentResponse>, IActivateDriverContract
    {
        public string NationalNumber { get; init; }
    }
}
