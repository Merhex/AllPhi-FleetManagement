using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class MotorVehicleRepository : Repository<MotorVehicle, int>, IMotorVehicleRepository
    {
        public MotorVehicleRepository(FleetManagementContext context) : base(context) { }

        public async Task<MotorVehicle> FindByChassisNumber(string chassisNumber, CancellationToken cancellationToken) =>
            await _context.MotorVehicles
                    .SingleOrDefaultAsync(m => m.ChassisNumber == chassisNumber, cancellationToken);

        public async Task<MotorVehicle> FindByIdAsync(int motorVehicleId, CancellationToken cancellationToken) =>
            await _context.MotorVehicles
                    .Include(motorVehicle => motorVehicle.LicensePlates)
                    .SingleOrDefaultAsync(m => m.Id == motorVehicleId, cancellationToken);

        public async Task<MotorVehicle> FindByLicensePlateIdAsync(int licensePlateId, CancellationToken cancellationToken) =>
            await _context.MotorVehicles
                    .Include(motorVehicle => motorVehicle.LicensePlates)
                    .Where(motorVehicle => motorVehicle.LicensePlates
                    .Any(licensePlate => licensePlate.Id == licensePlateId))
                    .SingleOrDefaultAsync(cancellationToken);    
    }
}
