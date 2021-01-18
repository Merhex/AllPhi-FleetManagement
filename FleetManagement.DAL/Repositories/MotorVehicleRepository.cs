using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class MotorVehicleRepository : Repository<MotorVehicle, int>, IMotorVehicleRepository
    {
        public MotorVehicleRepository(FleetManagementContext context) : base(context) { }

        public Task<MotorVehicle> FindByChassisNumber(string chassisNumber) =>
            _context.MotorVehicles
                .SingleOrDefaultAsync(m => m.ChassisNumber == chassisNumber);
    }
}
