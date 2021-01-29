using System;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class BusinessRuleFailureEventArgs : EventArgs
    {
        public List<string> Messages { get; set; } = new List<string>();
    }
}
