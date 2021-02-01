using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.BLL
{
    public class BusinessRuleResponse : IBusinessRuleResponse
    {
        public string Name { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public static BusinessRuleResponse Success => new BusinessRuleResponse { };
        public BusinessRuleResponse Failure(IEnumerable<string> messages)
        {
            Name = GetType().Name;
            Messages = messages.ToList();

            return this;
        }
    }
}
