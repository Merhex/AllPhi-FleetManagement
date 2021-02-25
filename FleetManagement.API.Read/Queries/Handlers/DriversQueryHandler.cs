using AutoMapper;
using FleetManagement.API.Read.Mappings;
using FleetManagement.DAL.NHibernate;
using FleetManagement.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.API.Read.Queries.Handlers
{
    public class DriversQueryHandler : IRequestHandler<DriversQuery, IPaginatedResponse<DriverResponse>>
    {
        private readonly IReadDriverSession _readSession;
        private readonly IMapper _mapper;

        public DriversQueryHandler(IReadDriverSession readSession, IMapper mapper)
        {
            _readSession = readSession;
            _mapper = mapper;
        }

        public async Task<IPaginatedResponse<DriverResponse>> Handle(DriversQuery query, CancellationToken cancellationToken)
        {
            var drivers = await _readSession.GetDrivers(query.Page, query.PageSize, cancellationToken);

            var mappedResult = _mapper.Map<IEnumerable<DriverResponse>>(drivers);
            var count = await _readSession.GetTotalCount(cancellationToken);

            return new PaginatedResponse<DriverResponse>
            {
                Items = mappedResult,
                TotalCount = count
            };
        }
    }
}
