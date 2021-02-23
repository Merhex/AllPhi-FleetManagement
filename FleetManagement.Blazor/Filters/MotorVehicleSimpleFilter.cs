using System.Collections.Generic;

namespace FleetManagement.Blazor.Filters
{
    public class MotorVehicleSimpleFilter
    {
        public string ChassisNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }

        public string GetFilterParameters()
        {
            var filterParameters = new List<string>();

            if (string.IsNullOrWhiteSpace(ChassisNumber) is false)
                filterParameters.Add($"ChassisNumber={ChassisNumber}");

            if (string.IsNullOrWhiteSpace(Model) is false)
                filterParameters.Add($"Model={Model}");

            if (string.IsNullOrWhiteSpace(Brand) is false)
                filterParameters.Add($"Brand={Brand}");

            return string.Join('&', filterParameters);
        }
    }
}