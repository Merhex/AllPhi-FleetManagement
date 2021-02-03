using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public interface IReadResponse<T>
    {
        public IList<T> Items { get; }
    }
}
