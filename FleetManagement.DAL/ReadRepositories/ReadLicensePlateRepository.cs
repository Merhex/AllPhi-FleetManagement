using AutoMapper;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using FleetManagement.ReadModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class ReadLicensePlateRepository : IReadLicensePlatesRepository
    {
        private readonly FleetManagementContext _context;
        private readonly IMapper _mapper;

        public ReadLicensePlateRepository(FleetManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LicensePlateDetailed> GetDetailedLicensePlate(string identifier, CancellationToken cancellationToken = default)
        {
            var licensePlate = await _context.Set<LicensePlate>()
                .Include(x => x.History)
                .Include(x => x.MotorVehicle)
                .SingleOrDefaultAsync(x => x.Identifier == identifier, cancellationToken);

            return _mapper.Map<LicensePlateDetailed>(licensePlate);
        }

        public async Task<IEnumerable<LicensePlate>> GetLicensePlates(int page, int pageSize, string sortBy = null, CancellationToken cancellation = default, params Expression<Func<LicensePlate, bool>>[] filters) =>
            await _context.Set<LicensePlate>()
                .AsQueryable()
                .AddFilters(filters)
                .SortBy(sortBy)
                .Pagination(page, pageSize)
                .ToListAsync();

        public async Task<int> GetTotalCount<T>(params Expression<Func<T, bool>>[] filters) where T : class =>
            await _context.Set<T>()
                .AsQueryable()
                .AddFilters(filters)
                .CountAsync();
    }
}
