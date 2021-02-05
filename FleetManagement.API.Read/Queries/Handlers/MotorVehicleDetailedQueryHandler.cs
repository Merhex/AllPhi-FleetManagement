using AutoMapper;
using FleetManagement.API.Read.Mappings;
using FleetManagement.DAL.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.API.Read.Queries.Handlers
{
    public class MotorVehicleDetailedQueryHandler : IRequestHandler<MotorVehicleDetailedQuery, MotorVehicleDetailedResponse>
    {
        private readonly IReadMotorVehicleRepository _readRepository;
        private readonly IMapper _mapper;

        public MotorVehicleDetailedQueryHandler(IReadMotorVehicleRepository readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<MotorVehicleDetailedResponse> Handle(MotorVehicleDetailedQuery query, CancellationToken cancellationToken)
        {
            var motorVehicle = await _readRepository.GetMotorVehicleDetailed(query.ChassisNumber, cancellationToken);

            return _mapper.Map<MotorVehicleDetailedResponse>(motorVehicle);
        }
    }
}
