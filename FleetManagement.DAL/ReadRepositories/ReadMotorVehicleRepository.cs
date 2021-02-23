using AutoMapper;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using FleetManagement.ReadModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class ReadMotorVehicleRepository : IReadMotorVehicleRepository
    {
        private readonly FleetManagementContext _context;
        private readonly IMapper _mapper;

        public ReadMotorVehicleRepository(FleetManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MotorVehicleLicensePlate>> GetMotorVehicles(
            int page = 1,
            int pageSize = 20,
            string sortString = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<MotorVehicle, bool>>[] filters)
        {
            var motorVehicles = await _context.MotorVehicles
                                    .Include(motorVehicle => motorVehicle.LicensePlates)
                                    .AddFilters(filters)
                                    .SortBy(sortString)
                                    .Pagination(page, pageSize)
                                    .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<MotorVehicleLicensePlate>>(motorVehicles);
        }

        public async Task<MotorVehicleDetailed> GetMotorVehicleDetailed(string chassisNumber, CancellationToken cancellationToken = default)
        {
            var motorVehicle = await _context.MotorVehicles
                                    .Include(motorVehicle => motorVehicle.Driver)
                                    .Include(motorVehicle => motorVehicle.MileageHistory
                                        .OrderBy(x => x.SnapshotDate))
                                    .Include(motorVehicle => motorVehicle.LicensePlates)
                                    .Include(motorVehicle => motorVehicle.Condition
                                        .OrderByDescending(x => x.CreationDate))
                                    .SingleOrDefaultAsync(motorVehicle => motorVehicle.ChassisNumber == chassisNumber, cancellationToken);

            return _mapper.Map<MotorVehicleDetailed>(motorVehicle);
        }

        public async Task<int> GetTotalCount<T>() where T : class =>
            await _context.Set<T>().CountAsync();

        public async Task<int> GetTotalCount<T>(CancellationToken cancellationToken = default, params Expression<Func<T, bool>>[] filters) where T : class =>
           await _context.Set<T>()
                    .AsQueryable()
                    .AddFilters(filters)
                    .CountAsync(cancellationToken);
    }
}
