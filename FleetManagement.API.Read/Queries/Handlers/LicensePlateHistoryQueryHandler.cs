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
    public class LicensePlateHistoryQueryHandler : IRequestHandler<LicensePlateHistoryQuery, PaginatedResponse<LicensePlateSnapshotResponse>>
    {
        private readonly IReadLicensePlatesRepository _repository;
        private readonly IMapper _mapper;

        public LicensePlateHistoryQueryHandler(IReadLicensePlatesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<LicensePlateSnapshotResponse>> Handle(LicensePlateHistoryQuery query, CancellationToken cancellationToken)
        {
            var filters = new List<Expression<Func<LicensePlateSnapshot, bool>>>();

            if (string.IsNullOrWhiteSpace(query.Identifier) is false)
                filters.Add(x => x.LicensePlate.Identifier.Equals(query.Identifier));
            if (string.IsNullOrWhiteSpace(query.ChassisNumber) is false)
                filters.Add(x => x.MotorVehicle.ChassisNumber.StartsWith(query.ChassisNumber));
            if (string.IsNullOrWhiteSpace(query.Brand) is false)
                filters.Add(x => x.MotorVehicle.Brand.StartsWith(query.Brand));
            if (string.IsNullOrWhiteSpace(query.Model) is false)
                filters.Add(x => x.MotorVehicle.Model.StartsWith(query.Model));
            if (query.InUse is not null)
                filters.Add(x => x.InUse == query.InUse);

            var snapshots = await _repository.GetLicensePlateHistory(query.Identifier, query.Page, query.PageSize, query.SortBy, cancellationToken, filters.ToArray());
            var count = await _repository.GetTotalCount(cancellationToken, filters.ToArray());
            var mappedResult = _mapper.Map<IEnumerable<LicensePlateSnapshotResponse>>(snapshots);

            return new PaginatedResponse<LicensePlateSnapshotResponse>()
            {
                Items = mappedResult,
                TotalCount = count
            };
        }
    }
}
