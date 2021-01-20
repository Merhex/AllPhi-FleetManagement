using FleetManagement.BLL.Shared.Interfaces;
using FleetManagement.Models;
using MediatR;
using System;

namespace FleetManagement.BLL.FuelCards.Commands
{
    public record CreateFuelCardCommand : IRequest<ICommandResponse>
    {
        public string CardNumber { get; init; }
        public int PinCode { get; init; }
        public DateTime ExpiryDate { get; init; }
        public FuelCardAuthenticationType AuthenticationType { get; init; }
        public MotorVehiclePropulsionType PropulsionTypes { get; init; }
    }
}
