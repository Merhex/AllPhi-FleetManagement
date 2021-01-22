using System;
using System.Text.RegularExpressions;

namespace FleetManagement.BLL.Persons.Validators
{
    /// <summary>
    /// All information about the specific implementation details of the card can be found here.
    /// https://nl.wikipedia.org/wiki/Rijksregisternummer
    /// </summary>
    public static class BelgianNationalNumberChecker
    {
        private static readonly int _modulus = 97;

        public static bool ValidateFormatting(string nationalNumber) =>
            new Regex("^[0-9]{2}[.]{1}[0-9]{2}[.]{1}[0-9]{2}[-]{1}[0-9]{3}[.]{1}[0-9]{2}$")
                .Match(nationalNumber)
                .Success;

        public static bool ValidateChecksum(DateTime birthdate, string nationalNumber)
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

        public static bool ValidateForContainingBirthdate(DateTime birthdate, string nationalNumber)
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


        #region PRIVATE
        private static string ExtractBirthdateString(this string text) =>
            text.Substring(0, text.IndexOf('-'));

        private static string AsTwoDigitYear(this int number) =>
            (number % 100).ToString("D2");

        private static string AsTwoDigits(this int number, int increment = 0) =>
            (number + increment).ToString("D2");

        private static string OnlyAsNumbers(this string text) =>
            new Regex("[^0-9]")
            .Replace(text, "");
        #endregion
    }
}
