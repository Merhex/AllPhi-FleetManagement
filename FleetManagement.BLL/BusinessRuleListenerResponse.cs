using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class BusinessRuleListenerResponse : IBusinessRuleListenerResponse
    {
        public bool Success => Failures.Count is 0;
        public IList<IBusinessRuleFailure> Failures { get; set; } = new List<IBusinessRuleFailure>();
    }
}
