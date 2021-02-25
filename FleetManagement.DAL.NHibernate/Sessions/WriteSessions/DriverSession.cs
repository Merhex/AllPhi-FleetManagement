using FleetManagement.Models;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public class DriverSession : BaseMapperSession, IDriverSession
    {
        public DriverSession(ISession session) : base (session) { }

        public async Task<Driver> GetDriverByNationalNumber(string nationalNumber, CancellationToken cancellationToken = default) =>
               await _session.Query<Driver>().SingleOrDefaultAsync(x => x.NationalNumber == nationalNumber, cancellationToken);
    }
}
