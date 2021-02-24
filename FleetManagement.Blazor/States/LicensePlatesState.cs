using FleetManagement.Blazor.Filters;

namespace FleetManagement.Blazor.States
{
    public class LicensePlatesState
    {
        public LicensePlateFilter Filter { get; set; }
        public FilterState FilterState { get; set; } = new FilterState();
    }
}
