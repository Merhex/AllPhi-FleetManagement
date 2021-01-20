using FleetManagement.Mappings;
using MediatR;
using System.Collections.Generic;

namespace FleetManagement.BLL.Drivers.Queries
{
    public class GetAllDriversQuery : IRequest<IEnumerable<DriverResponse>>
    {

    }
}
