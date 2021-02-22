using FleetManagement.API.Read.Mappings;
using MediatR;

namespace FleetManagement.API.Read.Queries
{
    public class LicensePlateDetailedQuery : IRequest<LicensePlateDetailedResponse>
    {
        public string Identifier { get; set; }
    }
}
