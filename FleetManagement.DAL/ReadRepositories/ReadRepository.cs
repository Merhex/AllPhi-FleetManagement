using AutoMapper;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.ReadModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class ReadRepository : IReadRepository
    {
        private readonly FleetManagementContext _context;
        private readonly IMapper _mapper;

        public ReadRepository(FleetManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MotorVehicleLicensePlate>> GetOperationalMotorVehicles(int page = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var motorVehicles = await _context.MotorVehicles
                                    .Include(motorVehicle => motorVehicle.LicensePlates)
                                    .Include(motorVehicle => motorVehicle.MileageHistory
                                        .OrderByDescending(x => x.Mileage))
                                    .Where(motorVehicle => motorVehicle.Operational)
                                    .Pagination(page, pageSize)
                                    .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<MotorVehicleLicensePlate>>(motorVehicles);
        }

        public async Task<MotorVehicleDetailed> GetMotorVehicleDetailed(string chassisNumber, CancellationToken cancellationToken = default)
        {
            var motorVehicle = await _context.MotorVehicles
                                    .Include(motorVehicle => motorVehicle.Driver)
                                    .Include(motorVehicle => motorVehicle.MileageHistory
                                        .OrderByDescending(x => x.Mileage))
                                    .Include(motorVehicle => motorVehicle.LicensePlates)
                                    .Include(motorVehicle => motorVehicle.Condition
                                        .OrderByDescending(x => x.CreationDate))
                                    .SingleOrDefaultAsync(motorVehicle => motorVehicle.ChassisNumber == chassisNumber, cancellationToken);

            return _mapper.Map<MotorVehicleDetailed>(motorVehicle);
        }
    }
}
