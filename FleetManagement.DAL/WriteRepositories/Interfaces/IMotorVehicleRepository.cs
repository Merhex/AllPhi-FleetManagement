using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IMotorVehicleRepository : IRepository<MotorVehicle, int>
    {
        Task<MotorVehicle> FindByChassisNumberIncludeLicensePlatesAsync(string chassisNumber, CancellationToken cancellationToken);
        Task<MotorVehicle> FindByChassisNumberAsync(string chassisNumber, CancellationToken cancellationToken);
        Task<MotorVehicle> FindByIdIncludeLicensePlatesAsync(int motorVehicleId, CancellationToken cancellationToken);
        Task<MotorVehicle> FindByLicensePlateIdentifierIncludeLicensePlatesAsync(string licensePlateIdentifier, CancellationToken cancellationToken);
        Task<MotorVehicle> FindByLicensePlateIdentifierAsync(string licensePlateIdentifier, CancellationToken cancellationToken);
    }
}
