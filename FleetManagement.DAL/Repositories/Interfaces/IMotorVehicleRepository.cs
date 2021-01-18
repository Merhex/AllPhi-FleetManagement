using FleetManagement.Models;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IMotorVehicleRepository : IRepository<MotorVehicle, int>
    {
        Task<MotorVehicle> FindByChassisNumber(string chassisNumber);
    }
}
