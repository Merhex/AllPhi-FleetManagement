using System;
using System.Collections.Generic;

namespace FleetManagement.Blazor.Filters
{
    public class DriverFilter : IFilterable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalNumber { get; set; }
        public ActivityFilterSelect Active { get; set; } = ActivityFilterSelect.All;

        public string GetFilterParameters()
        {
            var filterParameters = new List<string>();

            if (string.IsNullOrWhiteSpace(FirstName) is false)
                filterParameters.Add($"FirstName={FirstName}");

            if (string.IsNullOrWhiteSpace(LastName) is false)
                filterParameters.Add($"LastName={LastName}");

            if (string.IsNullOrWhiteSpace(NationalNumber) is false)
                filterParameters.Add($"NationalNumber={NationalNumber}");

            if (Active.Activity() is not null)
                filterParameters.Add($"Active={Active.Activity()}");

            return string.Join('&', filterParameters);
        }
    }
}