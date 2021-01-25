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

            RuleFor(person => person.ZipCode).InclusiveBetween(1000, 9999).NotEmpty();

            RuleFor(person => person.NationalNumber)
                .Must(HaveValidFormattedNationalNumber)
                .Must(HaveValidChecksumInNationalNumber)
                .Must(HaveValidBirthdateInNationalNumber);
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
