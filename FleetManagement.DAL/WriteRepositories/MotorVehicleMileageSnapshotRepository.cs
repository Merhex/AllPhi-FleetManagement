using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class MotorVehicleMileageSnapshotRepository : Repository<MotorVehicleMileageSnapshot, int>, IMotorVehicleMileageSnapshotRepository
    {
        public MotorVehicleMileageSnapshotRepository(FleetManagementContext context) : base(context) { }

        public async Task<MotorVehicleMileageSnapshot> GetLastMileageOfMotorVehicle(string chassisNumber, CancellationToken cancellationToken) =>
           await _context.MotorVehicleMileageSnapshots
                .Where(snapshot => snapshot.MotorVehicle.ChassisNumber == chassisNumber)
                .OrderByDescending(snapshot => snapshot.SnapshotDate)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
