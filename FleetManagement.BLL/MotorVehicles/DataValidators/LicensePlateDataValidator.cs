using FleetManagement.Models;
using FluentValidation;

namespace FleetManagement.BLL.MotorVehicles.Validators
{
    public class LicensePlateDataValidator : AbstractValidator<LicensePlate>
    {
        public LicensePlateDataValidator()
        {
            RuleFor(licensePlate => licensePlate.Identifier)
                .MaximumLength(9)
                .NotEmpty();
        }
    }
}
