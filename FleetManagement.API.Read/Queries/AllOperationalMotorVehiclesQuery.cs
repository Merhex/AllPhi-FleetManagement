using FleetManagement.API.Read.Mappings;
using FleetManagement.Models.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace FleetManagement.API.Read.Queries
{
    public class AllOperationalMotorVehiclesQuery : IRequest<IPaginatedResponse<MotorVehicleResponse>>, IPaginatedQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
