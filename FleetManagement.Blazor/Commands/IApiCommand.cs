using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Commands
{
    public interface IApiCommand
    {
        public string Endpoint { get; }
    }
}
