using FleetManagement.BLL.Persons.Validators;
using FleetManagement.BLL.Persons.Validators.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace FleetManagement.Tests.UnitTests
{
    [TestFixture]
    public class BelgianNumberValidatorTests
    {
        private IBelgianNationalNumberValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new BelgiumNationalNumberValidator();
        }


        [TestCase("95.07.14-489.68")]
        public void ValidateFormat_WithCorrectFormatting_ShouldReturnTrue(string nationalNumber)
        {
            _validator.ValidateNationalNumberFormatting(nationalNumber).Should().BeTrue();
        }


        [TestCase("1995.07.14-489.68")]
        [TestCase("95.07.14.489.68")]
        [TestCase("95-07-14.489.68")]
        [TestCase("95071448968")]
        public void ValidateFormat_WithIncorrectFormatting_ShouldReturnFalse(string nationalNumber)
        {
            _validator.ValidateNationalNumberFormatting(nationalNumber).Should().BeFalse();
        }


        [TestCase("95.07.14-489.68","1995-07-14")]
        public void ValidateCheckSum_WithCorrectSequence_ShouldReturnTrue(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberChecksum(nationalNumber, birthDate).Should().BeTrue();
        }


        [TestCase("95.07.14-489.00", "1995-07-14")]
        public void ValidateCheckSum_WithIncorrectChecksum_ShouldReturnFalse(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberChecksum(nationalNumber, birthDate).Should().BeFalse();
        }


        [TestCase("95.07.14-489.68", "2000-01-01")]
        public void ValidateCheckSum_WithNotMatchingBirthdate_ShouldReturnFalse(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberChecksum(nationalNumber, birthDate).Should().BeFalse();
        }


        [TestCase("95.07.14-000.68", "1995-07-14")]
        public void ValidateCheckSum_WithIncorrectFollowupNumber_ShouldReturnFalse(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberChecksum(nationalNumber, birthDate).Should().BeFalse();
        }


        [TestCase("20.01.01-555.65", "2020-01-01")]
        [TestCase("00.01.01-555.33", "2020-01-01")]
        public void ValidateChecksum_WhenPersonIsBornAfter2000SequenceGetsAddedA2BeforeCalculation_ShouldReturnTrue(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberChecksum(nationalNumber, birthDate).Should().BeTrue();
        }


        [TestCase("95.07.14-489.68", "1995-07-14")]
        public void ValidateForContainingBirthDate_WithMatchingBirthDate_ShouldReturnTrue(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberForContainingBirthdate(nationalNumber, birthDate).Should().BeTrue();
        }


        [TestCase("95.27.14-489.14", "1995-07-14")]
        public void ValidateForContainingBirthDate_WhenBirthDateMonthHas20AIncrementCorrespondingToOriginalBirthDate_ShouldReturnTrue(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberForContainingBirthdate(nationalNumber, birthDate).Should().BeTrue();
        }


        [TestCase("93.45.18-223.50", "1993-05-18")]
        public void ValidateForContainingBirthDate_WhenBirthDateMonthHas40AIncrementCorrespondingToOriginalBirthDate_ShouldReturnTrue(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberForContainingBirthdate(nationalNumber, birthDate).Should().BeTrue();
        }


        [TestCase("95.07.14-489.68", "2000-01-01")]
        public void ValidateForContainingBirthDate_WithWrongBirthDate_ShouldReturnFalse(string nationalNumber, DateTime birthDate)
        {
            _validator.ValidateNationalNumberForContainingBirthdate(nationalNumber, birthDate).Should().BeFalse();
        }
    }
}
