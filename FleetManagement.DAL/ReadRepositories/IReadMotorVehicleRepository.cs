using FleetManagement.Models;
using FleetManagement.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IReadMotorVehicleRepository : ITotalEntityCounter
    {
        Task<IEnumerable<MotorVehicleLicensePlate>> GetMotorVehicles(int page, int pageSize, string sortBy = null, CancellationToken cancellationToken = default, params Expression<Func<MotorVehicle, bool>>[] filters);
        Task<MotorVehicleDetailed> GetMotorVehicleDetailed(string chassisNumber, CancellationToken cancellationToken = default);
    }
}
