using FleetManagement.Models;
using FluentAssertions;
using FluentValidation;

namespace FleetManagement.BLL.Persons.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person.AddressLine).MaximumLength(100).NotEmpty();

            RuleFor(person => person.City).MaximumLength(50).NotEmpty();

            RuleFor(person => person.DateOfBirth).NotEmpty();

            RuleFor(person => person.FirstName).MaximumLength(50).NotEmpty();

            RuleFor(person => person.LastName).MaximumLength(50).NotEmpty();

            RuleFor(person => person.ZipCode).InclusiveBetween(1000, 9999);

            RuleFor(person => person.NationalNumber)
                .Must(HaveValidFormattedNationalNumber)
                    .WithMessage("The specified national number does not have the right format. The format should be as followed: XX.XX.XX-XXX.XX")
                .Must(HaveValidChecksumInNationalNumber)
                    .WithMessage("The specified national number does not have a valid checksum, please check that you entered the numbers correctly.")
                .Must(HaveValidBirthdateInNationalNumber)
                    .WithMessage("The birthdate or national number is not correct. Please check that you entered the numbers correctly.");
        }

        #region PRIVATE
        private static bool HaveValidFormattedNationalNumber(Person person, string nationalNumber) =>
            BelgianNationalNumberChecker.ValidateFormatting(person.NationalNumber);

        private static bool HaveValidChecksumInNationalNumber(Person person, string nationalNumber) =>
            BelgianNationalNumberChecker.ValidateChecksum(person.DateOfBirth, person.NationalNumber);

        private static bool HaveValidBirthdateInNationalNumber(Person person, string nationalNumber) =>
            BelgianNationalNumberChecker.ValidateForContainingBirthdate(person.DateOfBirth, person.NationalNumber);
        #endregion
    }
}
