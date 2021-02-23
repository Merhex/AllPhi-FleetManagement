using FleetManagement.API.Read.Mappings;
using MediatR;

namespace FleetManagement.API.Read.Queries
{
    public class LicensePlatesQuery : IRequest<IPaginatedResponse<LicensePlateResponse>>, IPaginatedQuery, ISortableQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }

        public string Identifier { get; set; }
        public bool? InUse { get; set; }
    }
}
