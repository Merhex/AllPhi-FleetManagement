using FleetManagement.BLL.Helpers;
using FleetManagement.BLL.Validators;
using FleetManagement.BLL.Validators.Interfaces;
using FleetManagement.Models;
using FluentValidation;
using NUnit.Framework;
using System;

namespace FleetManagement.Tests.UnitTests
{
    [TestFixture]
    public class BelgianNumberValidatorTests
    {
        private IBelgianNationalNumberValidator _systemUnderTest;

        [SetUp]
        public void SetUp()
        {
            _systemUnderTest = new BelgianNationalNumberValidator();
        }

        [TestCase("1995-07-14", "95.07.14-489.68")]
        [TestCase("1993-05-18", "93.05.18-223.61")]
        public void Validate_ReturnsTrue_WhenSuppliedValidBelgianNationalNumberAndCorrespondingBirthDate(DateTime birthdate, string nationalNumber)
        {
            Assert.That(_systemUnderTest.Validate(birthdate, nationalNumber), Is.True);
        }

        [TestCase("2020-01-01", "20.01.01-555.65")]
        [TestCase("2000-01-01", "00.01.01-223.74")]
        public void Validate_ReturnsTrue_WhenPersonIsBornAfter2000CheckSumAdds2BeforeCalculation(DateTime birthdate, string nationalNumber)
        {
            Assert.That(_systemUnderTest.Validate(birthdate, nationalNumber), Is.True);
        }

        [TestCase("1995-07-14", "95.27.14-489.14")]
        [TestCase("1993-05-18", "93.45.18-223.50")]
        public void Validate_ReturnsTrue_WhenBirthDateMonthHas20And40IncrementCorrespondingToOriginalBirthDate(DateTime birthdate, string nationalNumber)
        {
            Assert.That(_systemUnderTest.Validate(birthdate, nationalNumber), Is.True);
        }

        [TestCase("1995-07-14", "1995.07.14-489.68")]
        [TestCase("1995-07-14", "95.07.14.489.68")]
        [TestCase("1995-07-14", "95-07-14.489.68")]
        [TestCase("1993-05-18", "93051822361")]
        public void Validate_ReturnsFalse_IncorrectBelgianNationalNumberFormat(DateTime birthdate, string nationalNumber)
        {
            Assert.That(_systemUnderTest.Validate(birthdate, nationalNumber), Is.False);
        }

        [TestCase("1995-07-14", "80.27.14-489.14")]
        [TestCase("1993-05-18", "80.45.18-223.50")]
        public void Validate_ReturnsFalse_NotMatchingBirthDatewithBelgianNationalNumber(DateTime birthdate, string nationalNumber)
        {
            Assert.That(_systemUnderTest.Validate(birthdate, nationalNumber), Is.False);
        }

        [TestCase("1995-07-14", "80.27.14-489.00")]
        [TestCase("1993-05-18", "80.45.18-223.00")]
        public void Validate_ReturnsFalse_WhenCheckingNumerIsInvalid(DateTime birthdate, string nationalNumber)
        {
            Assert.That(_systemUnderTest.Validate(birthdate, nationalNumber), Is.False);
        }
    }
}
