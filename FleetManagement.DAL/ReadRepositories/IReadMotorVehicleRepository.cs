using FleetManagement.Models;
using FleetManagement.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IReadMotorVehicleRepository
    {
        Task<IEnumerable<MotorVehicleLicensePlate>> GetOperationalMotorVehicles(int page, int pageSize, CancellationToken cancellationToken, params Expression<Func<MotorVehicle, bool>>[] filters);
        Task<MotorVehicleDetailed> GetMotorVehicleDetailed(string chassisNumber, CancellationToken cancellationToken = default);
        Task<int> GetTotalCount<T>() where T : class;
        Task<int> GetTotalCount<T>(params Expression<Func<T, bool>>[] filters) where T : class;
    }
}
