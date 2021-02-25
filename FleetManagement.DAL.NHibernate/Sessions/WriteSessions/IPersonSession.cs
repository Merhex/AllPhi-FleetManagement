using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public interface IPersonSession : IMapperSession
    {
        Task<Person> GetPersonByNationalNumber(string nationalNumber, CancellationToken cancellationToken = default);
    }
}
