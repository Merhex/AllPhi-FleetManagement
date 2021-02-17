using FleetManagement.Models;
using NHibernate;
using NHibernate.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public class PersonSession : BaseMapperSession, IPersonSession
    {
        public PersonSession(ISession session) : base(session) { }

        public async Task<Person> GetPersonByNationalNumber(string nationalNumber, CancellationToken cancellationToken = default) =>
            await _session.Query<Person>()
            .SingleOrDefaultAsync(x => x.NationalNumber == nationalNumber, cancellationToken);
    }
}
