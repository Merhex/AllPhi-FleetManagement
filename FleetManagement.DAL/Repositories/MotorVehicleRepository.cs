using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class MotorVehicleRepository : Repository<MotorVehicle, int>, IMotorVehicleRepository
    {
        public MotorVehicleRepository(FleetManagementContext context) : base(context) { }

        public Task<MotorVehicle> FindByChassisNumber(string chassisNumber, CancellationToken cancellationToken) =>
            _context.MotorVehicles
                .SingleOrDefaultAsync(m => m.ChassisNumber == chassisNumber, cancellationToken);

        public Task<MotorVehicle> FindByIdAsync(int motorVehicleId, CancellationToken cancellationToken) =>
            _context.MotorVehicles
                .SingleOrDefaultAsync(m => m.Id == motorVehicleId, cancellationToken);
    }
}
