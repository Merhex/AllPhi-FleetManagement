﻿using FleetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public interface IReadDriverSession : IMapperSession, ITotalEntityCounter<Driver>
    {
        Task<IEnumerable<Driver>> GetDrivers(
            int page,
            int pageSize,
            string sortBy,
            CancellationToken cancellationToken = default,
            params Expression<Func<Driver, bool>>[] filters
        );
    }
}
