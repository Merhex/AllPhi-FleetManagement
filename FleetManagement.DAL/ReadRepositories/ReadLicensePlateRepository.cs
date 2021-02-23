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

        public ReadLicensePlateRepository(FleetManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LicensePlateSnapshot>> GetLicensePlateHistory(string identifier, int page, int pageSize, string sortBy = null, CancellationToken cancellationToken = default, params Expression<Func<LicensePlateSnapshot, bool>>[] filters) =>
            await _context.Set<LicensePlateSnapshot>()
                .Include(x => x.MotorVehicle)
                .AsQueryable()
                .Where(x => x.LicensePlate.Identifier == identifier)
                .AddFilters(filters)
                .SortBy(sortBy)
                .Pagination(page, pageSize)
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<LicensePlate>> GetLicensePlates(int page, int pageSize, string sortBy = null, CancellationToken cancellationToken = default, params Expression<Func<LicensePlate, bool>>[] filters) =>
            await _context.Set<LicensePlate>()
                .AsQueryable()
                .AddFilters(filters)
                .SortBy(sortBy)
                .Pagination(page, pageSize)
                .ToListAsync(cancellationToken);

        public async Task<int> GetTotalCount<T>(CancellationToken cancellationToken = default, params Expression<Func<T, bool>>[] filters) where T : class =>
            await _context.Set<T>()
                .AsQueryable()
                .AddFilters(filters)
                .CountAsync(cancellationToken);
    }
}
