using Blazorise.DataGrid;
using FleetManagement.Blazor.Filters;
using FleetManagement.Blazor.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.States
{
    public class FleetState
    {
        public MotorVehicleFilter Filter { get; set; }
        public FilterState FilterState { get; set; } = new FilterState();
    }
}
