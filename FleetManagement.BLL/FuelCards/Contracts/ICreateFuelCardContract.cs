using FleetManagement.Models;
using System;

namespace FleetManagement.BLL.FuelCards.Contracts
{
    public interface ICreateFuelCardContract
    {
        public string CardNumber { get; init; }
        public int PinCode { get; init; }
        public DateTime ExpiryDate { get; init; }
        public int AuthenticationType { get; init; }
        public int PropulsionTypes { get; init; }
    }
}
