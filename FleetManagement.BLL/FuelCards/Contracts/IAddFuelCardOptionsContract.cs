using System.Collections.Generic;

namespace FleetManagement.BLL.FuelCards.Contracts
{
    public interface IAddFuelCardOptionsContract
    {
        public string CardNumber { get; set; }
        public IEnumerable<string> Options { get; init; }
    }
}
