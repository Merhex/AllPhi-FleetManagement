using FleetManagement.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public class ReadDriverSession : BaseMapperSession, IReadDriverSession
    {
        public ReadDriverSession(ISession session) : base(session) { }

        public async Task<IEnumerable<Driver>> GetDrivers(int page, int pageSize, string sortBy, CancellationToken cancellationToken = default) =>
            await _session.Query<Driver>()
                .SortBy(sortBy)
                .Pagination(page, pageSize)
                .ToListAsync(cancellationToken);

        public async Task<int> GetTotalCount(CancellationToken cancellationToken = default, params Expression<Func<Driver, bool>>[] filters) =>
            await _session.Query<Driver>()
                .AddFilters(filters)
                .CountAsync(cancellationToken);
    }
}
