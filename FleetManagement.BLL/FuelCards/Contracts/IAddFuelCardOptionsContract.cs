using System.Collections.Generic;

namespace FleetManagement.BLL.FuelCards.Contracts
{
    public interface IAddFuelCardOptionsContract
    {
        public int FuelCardId { get; set; }
        public IEnumerable<string> Options { get; init; }
    }
}
