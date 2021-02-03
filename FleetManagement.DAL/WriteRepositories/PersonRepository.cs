using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.Repositories
{
    public class PersonRepository : Repository<Person, int>, IPersonRepository 
    {
        public PersonRepository(FleetManagementContext context) : base(context) { }

        public async Task<Person> FindByNationalNumberAsync(string nationalNumber, CancellationToken cancellationToken) =>
            await _context.Persons
                .SingleOrDefaultAsync(person => person.NationalNumber == nationalNumber, cancellationToken);
    }
}
