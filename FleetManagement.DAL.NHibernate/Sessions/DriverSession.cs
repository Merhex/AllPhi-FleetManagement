using FleetManagement.Models;
using NHibernate;
using System.Linq;

namespace FleetManagement.DAL.NHibernate
{
    public class DriverSession : BaseMapperSession, IDriverSession
    {
        public IQueryable<Driver> Drivers => _session.Query<Driver>();

        public DriverSession(ISession session) : base (session) { }
    }
}
