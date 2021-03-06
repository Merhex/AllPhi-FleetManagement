﻿using AutoMapper;
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
    public class LicensePlatesQueryHandler : IRequestHandler<LicensePlatesQuery, IPaginatedResponse<LicensePlateResponse>>
    {
        private readonly IReadLicensePlatesRepository _repository;
        private readonly IMapper _mapper;

        public LicensePlatesQueryHandler(IReadLicensePlatesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IPaginatedResponse<LicensePlateResponse>> Handle(LicensePlatesQuery request, CancellationToken cancellationToken)
        {
            var filters = GetFilters(request);

            var result = await _repository.GetLicensePlates(request.Page, request.PageSize, request.SortBy, cancellationToken, filters.ToArray());
            var count = await _repository.GetTotalCount(cancellationToken, filters.ToArray());
            var mappedResult = _mapper.Map<IEnumerable<LicensePlateResponse>>(result);

            return new PaginatedResponse<LicensePlateResponse>
            {
                Items = mappedResult,
                TotalCount = count
            };
        }

        private static List<Expression<Func<LicensePlate, bool>>> GetFilters(LicensePlatesQuery request)
        {
            var filters = new List<Expression<Func<LicensePlate, bool>>>();

            if (string.IsNullOrWhiteSpace(request.Identifier) is false)
                filters.Add(x => x.Identifier.StartsWith(request.Identifier));

            if (request.InUse.HasValue)
                filters.Add(x => x.InUse == request.InUse);
            return filters;
        }
    }
}
