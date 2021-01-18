namespace FleetManagement.Models
{
    public abstract class FuelCardWorkOrder : WorkOrder<FuelCard>
    {
        public FuelCardWorkOrder() : this(new FuelCard()) { }

        public FuelCardWorkOrder(FuelCard subject) : base(subject) { }
    }
}
