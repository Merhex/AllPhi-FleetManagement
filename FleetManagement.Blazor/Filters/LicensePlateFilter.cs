using System;
using System.Collections.Generic;

namespace FleetManagement.Blazor.Filters
{
    public class LicensePlateFilter : IFilterable
    {
        public string Identifier { get; set; }
        public ActivityFilterSelect InUse { get; set; } = ActivityFilterSelect.All;

        public string GetFilterParameters()
        {
            var filterParameters = new List<string>();

            if (string.IsNullOrWhiteSpace(Identifier) is false)
                filterParameters.Add($"Identifier={Identifier}");

            if (InUse.Activity() is not null)
                filterParameters.Add($"InUse={InUse.Activity()}");

            return string.Join('&', filterParameters);
        }
    }
}