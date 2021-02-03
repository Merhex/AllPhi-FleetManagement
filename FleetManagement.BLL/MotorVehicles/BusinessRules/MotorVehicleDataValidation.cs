using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleDataValidation : IBusinessRule
    {
        private readonly MotorVehicleDataValidator _validator;
        private readonly MotorVehicle _motorVehicle;

        public MotorVehicleDataValidation(MotorVehicleDataValidator validator, MotorVehicle motorVehicle)
        {
            _validator = validator;
            _motorVehicle = motorVehicle;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(_motorVehicle, cancellationToken);

            if (result.IsValid is false)
                return new BusinessRuleResponse()
                    .ConvertValidationResult(this, result);

            return BusinessRuleResponse.Success;
        }
    }
}
