using System;
using System.Text.RegularExpressions;
using FleetManagement.BLL.Shared.Helpers;
using FleetManagement.BLL.Shared.Validators.Interfaces;
using FluentValidation;

namespace FleetManagement.BLL.Shared.Validators
{
    /// <summary>
    /// All information about the specific implementation details of the card can be found here.
    /// https://nl.wikipedia.org/wiki/Rijksregisternummer
    /// </summary>
    public class BelgianNationalNumberValidator : IBelgianNationalNumberValidator
    {
        private readonly int _modulus = 97;

        public bool ValidateFormat(string nationalNumber) => HasCorrectFormat(nationalNumber);
        public bool Validate(DateTime birthdate, string nationalNumber) => ValidateWholeCard(birthdate, nationalNumber);
   

        #region PRIVATE IMPLEMENTATION DETAIL
        private bool ValidateWholeCard(DateTime birthdate, string nationalNumber)
        {
            if (HasCorrectFormat(nationalNumber))
                return HasCorrectPossibleNationalNumberDates(birthdate, nationalNumber) &&
                       HasValidCheckSum(birthdate, nationalNumber);

            else return false;
        }

        private bool HasValidCheckSum(DateTime birthdate, string nationalNumber)
        {
            var numbers = nationalNumber.OnlyAsNumbers();

            var sequenceToBeChecked = numbers[0..9];
            var checksumAsString = numbers[9..11];

            if (birthdate.Year >= 2000) 
                sequenceToBeChecked = sequenceToBeChecked.Insert(0, "2");

            var sequence = long.Parse(sequenceToBeChecked);
            var checksum = int.Parse(checksumAsString);
            var moduloResult = sequence % _modulus;

            return _modulus - moduloResult == checksum;
        }

        private static bool HasCorrectPossibleNationalNumberDates(DateTime birthdate, string nationalNumber)
        {
            var month = birthdate.Month;
            var day = birthdate.Day.AsTwoDigits();
            var year = birthdate.Year.AsTwoDigitYear();
            var nationalNumberBirthDate = nationalNumber.ExtractBirthdateString();

            var possibleDateFormats = new string[]
            { 
                $"{year}.{month.AsTwoDigits()}.{day}",
                $"{year}.{month.AsTwoDigits(increment:20)}.{day}",
                $"{year}.{month.AsTwoDigits(increment:40)}.{day}"
            };

            foreach (var possibleDate in possibleDateFormats)
                if (nationalNumberBirthDate.Equals(possibleDate))
                    return true;

            return false;
        }

        private static bool HasCorrectFormat(string nationalNumber) =>
            new Regex("^[0-9]{2}[.]{1}[0-9]{2}[.]{1}[0-9]{2}[-]{1}[0-9]{3}[.]{1}[0-9]{2}$")
                .Match(nationalNumber)
                .Success;
        #endregion
    }
}
