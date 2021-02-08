using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Filters
{
    public class MotorVehicleFilter
    {
        public string ChassisNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }

        public string GetQueryParamaters()
        {
            var builder = new StringBuilder("");

            if (ChassisNumber is not null)
                builder.Append($"&ChassisNumber={ChassisNumber}");

            if (Model is not null)
                builder.Append($"&Model={Model}");

            if (Brand is not null)
                builder.Append($"&Brand={Brand}");

            return builder.ToString();
        }
    }
}
