using System.Collections.Generic;

namespace FleetManagement.Blazor.Filters
{
    public class LicensePlateFilter : IFilterable
    {
        public string Identifier { get; set; }
        public bool InUse { get; set; } = false;

        public string GetFilterParameters()
        {
            var filterParameters = new List<string>();

            if (string.IsNullOrWhiteSpace(Identifier) is false)
                filterParameters.Add($"Identifier={Identifier}");

            filterParameters.Add($"InUse={InUse}");

            return string.Join('&', filterParameters);
        }
    }
}