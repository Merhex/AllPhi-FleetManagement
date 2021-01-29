namespace FleetManagement.BLL
{
    public class BusinessRuleFailure : IBusinessRuleFailure
    {
        public string Rule { get; set; }
        public string ErrorMessage { get; set; }
    }
}
