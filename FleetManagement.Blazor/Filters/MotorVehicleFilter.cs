﻿using System;
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
        public ActivityFilterSelect Operational { get; set; } = ActivityFilterSelect.All;

        public string GetFilterParameters()
        {
            var filterParameters = new List<string>();

            if (string.IsNullOrWhiteSpace(ChassisNumber) is false)
                filterParameters.Add($"ChassisNumber={ChassisNumber}");

            if (string.IsNullOrWhiteSpace(Model) is false)
                filterParameters.Add($"Model={Model}");

            if (string.IsNullOrWhiteSpace(Brand) is false)
                filterParameters.Add($"Brand={Brand}");
            
            if (Operational.Activity() is not null)
                filterParameters.Add($"Operational={Operational.Activity()}");

            return string.Join('&', filterParameters);
        }
    }
}