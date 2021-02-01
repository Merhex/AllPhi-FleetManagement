using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public interface IBusinessHandlerResponse
    {
        public bool Success { get; }
        public List<IBusinessRuleResponse> Responses { get; set; }
    }
}
