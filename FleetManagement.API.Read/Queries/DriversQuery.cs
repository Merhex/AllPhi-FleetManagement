using FleetManagement.API.Read.Mappings;
using MediatR;
using System;

namespace FleetManagement.API.Read.Queries
{
    public class DriversQuery : IRequest<IPaginatedResponse<DriverResponse>>, IPaginatedQuery, ISortableQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalNumber { get; set; }
        public bool? Active { get; set; }
    }
}
