using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Helpers
{
    public static class Extensions
    {
        public static string ExtractBirthdateString(this string text) =>
            text.Substring(0, text.IndexOf('-'));

        public static string AsTwoDigitYear(this int number) =>
            (number % 100).ToString("D2");

        public static string AsTwoDigits(this int number, int increment = 0) =>
            (number + increment).ToString("D2");

        public static string OnlyAsNumbers(this string text) =>
            new Regex("[^0-9]")
            .Replace(text, "");
    }
}
