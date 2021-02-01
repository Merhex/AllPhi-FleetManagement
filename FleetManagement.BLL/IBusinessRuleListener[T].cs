namespace FleetManagement.BLL
{
    public interface IBusinessRuleListener<T> where T : IContract
    {
        public bool Success { get; }
        public IBusinessRuleListenerResponse Speak();
    }
}
