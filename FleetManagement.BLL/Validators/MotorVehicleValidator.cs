using FleetManagement.Models;
using FluentValidation;

namespace FleetManagement.BLL.Validators
{
    public class MotorVehicleValidator : AbstractValidator<MotorVehicle>
    {
        public MotorVehicleValidator()
        {
            RuleFor(motorVehicle => motorVehicle.ChassisNumber)
                .Matches("[A-HJ-NPR-Z0-9]{17}")
                .WithMessage("The given VIN number is incorrect. Please check the formatting.");

            RuleFor(motorVehicle => motorVehicle.BodyType)
                .IsInEnum()
                .WithMessage("The given body type is not a valid option out of the option list.");

            RuleFor(motorVehicle => motorVehicle.PropulsionType)
                .IsInEnum()
                .WithMessage("The given propulsion type is not a valid option out of the option list.");

            RuleFor(motorVehicle => motorVehicle.Brand)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(motorVehicle => motorVehicle.Model)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
