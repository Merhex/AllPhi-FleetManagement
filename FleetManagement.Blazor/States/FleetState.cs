using FleetManagement.Blazor.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.States
{
    public class FleetState
    {
        public MotorVehicleFilter Filter { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool FilterIsVisible { get; set; }
    }
}
