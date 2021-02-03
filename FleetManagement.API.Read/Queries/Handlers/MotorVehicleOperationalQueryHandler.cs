using AutoMapper;
using FleetManagement.API.Read.Mappings;
using FleetManagement.DAL.Repositories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.API.Read.Queries.Handlers
{
    public class MotorVehicleOperationalQueryHandler : IRequestHandler<MotorVehicleOperationalQuery, IPaginatedResponse<MotorVehicleResponse>>
    {
        private readonly IReadRepository _readRepository;
        private readonly IMapper _mapper;

        public MotorVehicleOperationalQueryHandler(IReadRepository readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<IPaginatedResponse<MotorVehicleResponse>> Handle(MotorVehicleOperationalQuery query, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetOperationalMotorVehicles(query.Page, query.PageSize, cancellationToken);

            var mappedResult = _mapper.Map<IEnumerable<MotorVehicleResponse>>(result);

            return new PaginatedResponse<MotorVehicleResponse>() 
            { 
                Items = mappedResult,
                Page = query.Page,
                PageSize = query.PageSize 
            };
        }
    }
}
