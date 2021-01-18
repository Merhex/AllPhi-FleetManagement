namespace FleetManagement.Models
{
    public class FuelCardRequestWorkOrder : FuelCardWorkOrder
    {
        public FuelCardRequestWorkOrder() : this(new FuelCard()) { }

        private FuelCardRequestWorkOrder(FuelCard subject) : base(subject) { }
    }
}
