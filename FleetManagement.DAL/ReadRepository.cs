using AutoMapper;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using FleetManagement.Models.ReadModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<MotorVehicleLicensePlates>> GetOperationalMotorVehicles(int page = 1, int pageSize = 20)
        {
            var motorVehicles = await _context.MotorVehicles
                                    .Include(motorVehicle => motorVehicle.LicensePlates)
                                    .Where(motorVehicle => motorVehicle.Operational)
                                    .Pagination(page, pageSize)
                                    .ToListAsync();

            return _mapper.Map<IEnumerable<MotorVehicleLicensePlates>>(motorVehicles);
        }
    }
}
