using FleetManagement.API.Read.Mappings;
using MediatR;

namespace FleetManagement.API.Read.Queries
{
    public class LicensePlateHistoryQuery : IRequest<PaginatedResponse<LicensePlateSnapshotResponse>>, IPaginatedQuery, ISortableQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }

        public string Identifier { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ChassisNumber { get; set; }
        public bool? InUse { get; set; }
    }
}
