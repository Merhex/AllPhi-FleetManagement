using FleetManagement.BLL.Persons.Components.Interfaces;
using FleetManagement.BLL.Persons.Contracts;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.Components
{
    public class PersonComponent : IPersonComponent
    {
        private readonly PersonValidator _personValidator;
        private readonly IPersonRepository _personRepository;

        public PersonComponent(
            PersonValidator personValidator,
            IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<IComponentResponse> UpdatePersonInformationAsync(IUpdatePersonInformationContract contract, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
