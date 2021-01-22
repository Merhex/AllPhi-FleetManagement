using System;
using FleetManagement.BLL;
using FleetManagement.BLL.FuelCards.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record CreateFuelCardCommand : IRequest<IComponentResponse>, ICreateFuelCardContract
    {
        public string CardNumber { get; init; }
        public int PinCode { get; init; }
        public DateTime ExpiryDate { get; init; }
        public int AuthenticationType { get; init; }
        public int PropulsionTypes { get; init; }
    }
}
