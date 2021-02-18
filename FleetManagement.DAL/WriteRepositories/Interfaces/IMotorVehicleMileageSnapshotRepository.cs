using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IMotorVehicleMileageSnapshotRepository : IRepository<MotorVehicleMileageSnapshot, int>
    {
        Task<MotorVehicleMileageSnapshot> GetLastMileageOfMotorVehicle(string chassisNumber, CancellationToken cancellationToken);
    }
}
