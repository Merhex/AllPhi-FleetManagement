using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public interface IBusinessRuleResponse
    {
        public string Name { get; set; }
        List<string> Messages { get; }
    }
}
