using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleDataValidation : IBusinessRule<ICreateMotorVehicleContract>
    {
        private readonly MotorVehicleDataValidator _validator;

        public event BusinessRuleFailureEventHandler<ICreateMotorVehicleContract> Failure;

        public MotorVehicleDataValidation(MotorVehicleDataValidator validator)
        {
            _validator = validator;
        }

        public async Task Handle(ICreateMotorVehicleContract contract, CancellationToken token = default)
        {
            var motorVehicle = new MotorVehicle
            {
                BodyType       = (MotorVehicleBodyType) contract.BodyType,
                Brand          = contract.Brand,
                ChassisNumber  = contract.ChassisNumber,
                Model          = contract.Model,
                Operational    = contract.Operational,
                PropulsionType = (MotorVehiclePropulsionType) contract.PropulsionType
            };

            var validation = await _validator.ValidateAsync(motorVehicle, token);

            if (validation.IsValid is false)
            {
                var arguments = new BusinessRuleFailureEventArgs();

                foreach (var error in validation.Errors)
                    arguments.Messages.Add(error.ErrorMessage);

                OnFailure(arguments);
            }
        }

        protected virtual void OnFailure(BusinessRuleFailureEventArgs args)
        {
            Failure?.Invoke(this, args);
        }
    }
}
