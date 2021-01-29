namespace FleetManagement.BLL
{
    public interface IBusinessRuleFailure
    {
        public string Rule { get; set; }
        public string ErrorMessage { get; set; }
    }
}
