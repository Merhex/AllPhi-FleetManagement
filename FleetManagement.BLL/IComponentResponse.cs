using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public interface IComponentResponse
    {
        public IDictionary<string, ICollection<string>> Failures { get; }
    }
}
