using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateDataValidation : IBusinessRule
    {
        private readonly LicensePlateDataValidator _validator;
        private readonly LicensePlate _licensePlate;

        public LicensePlateDataValidation(LicensePlateDataValidator validator, LicensePlate licensePlate)
        {
            _validator = validator;
            _licensePlate = licensePlate;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(_licensePlate, cancellationToken);

            if (result.IsValid is false)
                return new BusinessRuleResponse()
                    .ConvertValidationResult(result);

            return BusinessRuleResponse.Success;
        }
    }
}
