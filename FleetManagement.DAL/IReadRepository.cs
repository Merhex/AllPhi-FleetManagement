using FleetManagement.Models;
using FleetManagement.Models.ReadModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IReadRepository
    {
        Task<IEnumerable<MotorVehicleLicensePlates>> GetOperationalMotorVehicles(int page = 1, int pageSize = 20);
    }
}
