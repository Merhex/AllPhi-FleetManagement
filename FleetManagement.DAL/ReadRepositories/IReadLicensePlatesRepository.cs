﻿using FleetManagement.Models;
using FleetManagement.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IReadLicensePlatesRepository : ITotalEntityCounter
    {
        Task<IEnumerable<LicensePlate>> GetLicensePlates(int page, int pageSize, string sortBy = null, CancellationToken cancellation = default, params Expression<Func<LicensePlate, bool>>[] filters);
        Task<IEnumerable<LicensePlateSnapshot>> GetLicensePlateHistory(
            string identifier,
            int page,
            int pageSize,
            string sortBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<LicensePlateSnapshot, bool>>[] filters);
    }
}
