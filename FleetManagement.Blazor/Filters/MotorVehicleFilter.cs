using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Filters
{
    public class MotorVehicleFilter : IFilterable
    {
        public string ChassisNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public bool Operational { get; set; } = true;

        public string GetFilterParameters()
        {
            var filterParameters = new List<string>();

            if (ChassisNumber is not null)
                filterParameters.Add($"ChassisNumber={ChassisNumber}");

            if (Model is not null)
                filterParameters.Add($"Model={Model}");

            if (Brand is not null)
                filterParameters.Add($"Brand={Brand}");

            filterParameters.Add($"Operational={Operational}");

            return string.Join('&', filterParameters);
        }
    }
}