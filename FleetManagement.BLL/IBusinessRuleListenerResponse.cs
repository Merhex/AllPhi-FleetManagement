using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public interface IBusinessRuleListenerResponse
    {
        public IList<IBusinessRuleFailure> Failures { get; set; }
        public bool Success { get; }
    }
}
