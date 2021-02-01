using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class CheckMotorVehicleData : IBusinessRule
    {
        private readonly MotorVehicleDataValidator _validator;
        private readonly MotorVehicle _motorVehicle;

        public CheckMotorVehicleData(MotorVehicleDataValidator validator, MotorVehicle motorVehicle)
        {
            _validator = validator;
            _motorVehicle = motorVehicle;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(_motorVehicle, cancellationToken);

            if (result.IsValid is false)
            {
                var response = new BusinessRuleResponse { Name = GetType().Name };
                foreach (var error in result.Errors)
                    response.Messages.Add(error.ErrorMessage);

                return response;
            }

            return BusinessRuleResponse.Success;
        }
    }
}
