using AutoMapper;
using FleetManagement.API.Read.Mappings;
using FleetManagement.DAL.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.API.Read.Queries.Handlers
{
    public class LicensePlateDetailedQueryHandler : IRequestHandler<LicensePlateDetailedQuery, LicensePlateDetailedResponse>
    {
        private readonly IReadLicensePlatesRepository _repository;
        private readonly IMapper _mapper;

        public LicensePlateDetailedQueryHandler(IReadLicensePlatesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LicensePlateDetailedResponse> Handle(LicensePlateDetailedQuery request, CancellationToken cancellationToken)
        {
            var licensePlate = await _repository.GetDetailedLicensePlate(request.Identifier, cancellationToken);

            return _mapper.Map<LicensePlateDetailedResponse>(licensePlate);
        }
    }
}
