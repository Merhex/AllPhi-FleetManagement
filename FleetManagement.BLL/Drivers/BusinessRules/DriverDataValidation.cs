using FleetManagement.BLL.Persons.Validators;
using FleetManagement.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.BusinessRules
{
    public class DriverDataValidation : IBusinessRule
    {
        private readonly PersonValidator _validator;
        private readonly Driver _driver;

        public DriverDataValidation(PersonValidator validator, Driver driver)
        {
            _validator = validator;
            _driver = driver;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(_driver, cancellationToken);

            if (validationResult.Errors.Any())
                return new BusinessRuleResponse()
                    .ConvertValidationResult(this, validationResult);
            else
                return BusinessRuleResponse.Success;
        }
    }
}
