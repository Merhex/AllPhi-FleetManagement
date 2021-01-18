using FleetManagement.Mappings;
using MediatR;
using System.Collections.Generic;

namespace FleetManagement.API.Read.Queries
{
    public class GetAllDriversQuery : IRequest<IEnumerable<DriverResponse>>
    {

    }
}
