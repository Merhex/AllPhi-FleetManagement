using AutoMapper;
using FleetManagement.API.Read.Mappings;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.API.Read.Queries.Handlers
{
    public class MotorVehicleOperationalQueryHandler : IRequestHandler<MotorVehicleOperationalQuery, IPaginatedResponse<MotorVehicleResponse>>
    {
        private readonly IReadMotorVehicleRepository _readRepository;
        private readonly IMapper _mapper;

        public MotorVehicleOperationalQueryHandler(IReadMotorVehicleRepository readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<IPaginatedResponse<MotorVehicleResponse>> Handle(MotorVehicleOperationalQuery query, CancellationToken cancellationToken)
        {
            var filters = new List<Expression<Func<MotorVehicle, bool>>>();

            if (query.Brand is not null)
                filters.Add(x => x.Brand.Contains(query.Brand));
            if (query.ChassisNumber is not null)
                filters.Add(x => x.ChassisNumber.Contains(query.ChassisNumber));
            if (query.Model is not null)
                filters.Add(x => x.Model.Contains(query.Model));

            var result = await _readRepository.GetOperationalMotorVehicles(query.Page, query.PageSize, cancellationToken, filters.ToArray());
            var count = await _readRepository.GetTotalCount(filters.ToArray());

            var mappedResult = _mapper.Map<IEnumerable<MotorVehicleResponse>>(result);

            return new PaginatedResponse<MotorVehicleResponse>()
            {
                Items = mappedResult,
                TotalCount = count
            };
        }
    }
}
