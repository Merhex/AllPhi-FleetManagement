using FleetManagement.ReadModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IReadRepository
    {
        Task<IEnumerable<MotorVehicleLicensePlate>> GetOperationalMotorVehicles(int page, int pageSize, CancellationToken cancellationToken);
        Task<MotorVehicleDetailed> GetMotorVehicleDetailed(string chassisNumber, CancellationToken cancellationToken = default);
    }
}
