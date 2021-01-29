namespace FleetManagement.BLL
{
    public delegate void BusinessRuleFailureEventHandler<T>(IBusinessRule<T> source, BusinessRuleFailureEventArgs args) where T : IContract;
}
