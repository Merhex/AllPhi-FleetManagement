using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.BLL
{
    public class BusinessHandlerResponse : IBusinessHandlerResponse
    {
        public bool Success => IsSuccesful;

        public List<IBusinessRuleResponse> Responses { get; set; } = new List<IBusinessRuleResponse>();

        #region PRIVATE
        private bool IsSuccesful => Responses.All(x => x.Name is null);
        #endregion
    }
}
