using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.WinForms.Queries
{
    public interface IQuery
    {
        public string Endpoint { get; }
    }
}
