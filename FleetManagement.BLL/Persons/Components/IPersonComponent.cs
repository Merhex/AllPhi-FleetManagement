using FleetManagement.BLL.Persons.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.Components.Interfaces
{
    public interface IPersonComponent
    {
        Task<IComponentResponse> UpdatePersonInformationAsync(IUpdatePersonInformationContract contract, CancellationToken cancellationToken);
    }
}
