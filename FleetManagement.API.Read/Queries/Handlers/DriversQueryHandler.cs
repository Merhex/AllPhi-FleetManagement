using AutoMapper;
using FleetManagement.API.Read.Mappings;
using FleetManagement.DAL.NHibernate;
using FleetManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            var filters = GetFilters(query);

            var drivers = await _readSession.GetDrivers(query.Page, query.PageSize, query.SortBy, cancellationToken, filters.ToArray());

            var mappedResult = _mapper.Map<IEnumerable<DriverResponse>>(drivers);
            var count = await _readSession.GetTotalCount(cancellationToken, filters.ToArray());

            return new PaginatedResponse<DriverResponse>
            {
                Items = mappedResult,
                TotalCount = count
            };
        }


        private static List<Expression<Func<Driver, bool>>> GetFilters(DriversQuery query)
        {
            var filters = new List<Expression<Func<Driver, bool>>>();

            if (query.FirstName is not null)
                filters.Add(x => x.FirstName.Contains(query.FirstName));
            if (query.LastName is not null)
                filters.Add(x => x.LastName.Contains(query.LastName));
            if (query.NationalNumber is not null)
                filters.Add(x => x.NationalNumber.Contains(query.NationalNumber));
            if (query.Active is not null)
                filters.Add(x => x.Active == query.Active);

            return filters;
        }
    }
}
