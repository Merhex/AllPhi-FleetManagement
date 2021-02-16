using FleetManagement.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public interface IDriverSession : IMapperSession
    {
        Task<Driver> GetDriverByNationalNumber(string nationalNumber, CancellationToken cancellationToken = default);
    }
}
