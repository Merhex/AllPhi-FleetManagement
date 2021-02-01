using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class BusinessRuleResponse : IBusinessRuleResponse
    {
        public string Name { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public static BusinessRuleResponse Success => new BusinessRuleResponse { };
    }
}
