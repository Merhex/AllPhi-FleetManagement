using FleetManagement.API.Read.Mappings;
using MediatR;

namespace FleetManagement.API.Read.Queries
{
    public class MotorVehicleOperationalQuery : IRequest<IPaginatedResponse<MotorVehicleResponse>>, IPaginatedQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
