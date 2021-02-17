using FleetManagement.BLL.Persons.Validators;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.BusinessRules
{
    public class PersonDataValidation : IBusinessRule
    {
        private readonly PersonValidator _validator;
        private readonly Person _person;

        public PersonDataValidation(PersonValidator validator, Person person)
        {
            _validator = validator;
            _person = person;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(_person, cancellationToken);

            if (validationResult.IsValid is false)
                return new BusinessRuleResponse()
                    .ConvertValidationResult(this, validationResult);
            else
                return BusinessRuleResponse.Success;
        }
    }
}
