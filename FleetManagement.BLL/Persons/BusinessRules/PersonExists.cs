using FleetManagement.DAL.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.BusinessRules
{
    public class PersonExists : IBusinessRule
    {
        private readonly IPersonSession _session;
        private readonly string _nationalNumber;

        public PersonExists(IPersonSession session, string nationalNumber)
        {
            _session = session;
            _nationalNumber = nationalNumber;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var person = await _session.GetPersonByNationalNumber(_nationalNumber, cancellationToken);

            if (person is null)
                return new BusinessRuleResponse()
                    .Failure(this, $"The person with given national number: {_nationalNumber}, does not exist.");
            else
                return BusinessRuleResponse.Success;
        }
    }
}
