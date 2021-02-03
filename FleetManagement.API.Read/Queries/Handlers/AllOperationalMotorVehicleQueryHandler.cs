using AutoMapper;
using FleetManagement.API.Read.Mappings;
using FleetManagement.DAL.Repositories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.API.Read.Queries.Handlers
{
    public class AllOperationalMotorVehicleQueryHandler : IRequestHandler<AllOperationalMotorVehiclesQuery, IPaginatedResponse<MotorVehicleResponse>>
    {
        private readonly IReadRepository _readRepository;
        private readonly IMapper _mapper;

        public AllOperationalMotorVehicleQueryHandler(IReadRepository readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<IPaginatedResponse<MotorVehicleResponse>> Handle(AllOperationalMotorVehiclesQuery request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetOperationalMotorVehicles(request.Page, request.PageSize);

            var mappedResult = _mapper.Map<IEnumerable<MotorVehicleResponse>>(result);

            return new PaginatedResponse<MotorVehicleResponse>() 
            { 
                Items = mappedResult,
                Page = request.Page,
                PageSize = request.PageSize 
            };
        }
    }
}
