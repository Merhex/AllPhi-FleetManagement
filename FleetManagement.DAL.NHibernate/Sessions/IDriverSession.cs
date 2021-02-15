using FleetManagement.Models;
using System.Linq;

namespace FleetManagement.DAL.NHibernate
{
    public interface IDriverSession : IMapperSession
    {
        IQueryable<Driver> Drivers { get; }
    }
}
