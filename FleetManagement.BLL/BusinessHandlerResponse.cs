using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class BusinessHandlerResponse : IBusinessHandlerResponse
    {
        public bool Success => Responses.Count is 0;

        public List<IBusinessRuleResponse> Responses { get; set; } = new List<IBusinessRuleResponse>();
    }
}
