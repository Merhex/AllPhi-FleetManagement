using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateDataValidation : IBusinessRule<ILicensePlateContract>
    {
        public event BusinessRuleFailureEventHandler<ILicensePlateContract> Failure;
        
        private readonly LicensePlateDataValidator _validator;
        
        public LicensePlateDataValidation(LicensePlateDataValidator validator)
        {
            _validator = validator;
        }

        public async Task Handle(ILicensePlateContract contract, CancellationToken token = default)
        {
            var licensePlate = new LicensePlate { Identifier = contract.Identifier };

            var validation = await _validator.ValidateAsync(licensePlate, token);

            if (validation.IsValid is false)
            {
                var arguments = new BusinessRuleFailureEventArgs();

                foreach (var error in validation.Errors)
                    arguments.Messages.Add(error.ErrorMessage);

                OnFailure(arguments);
            }
        }

        private void OnFailure(BusinessRuleFailureEventArgs args)
        {
            Failure?.Invoke(this, args);
        }
    }
}
