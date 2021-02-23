using System.Collections.Generic;

namespace FleetManagement.Blazor.Filters
{
    public class LicensePlateHistoryFilter
    {
        public MotorVehicleSimpleFilter MotorVehicleSimpleFilter { get; set; } = new MotorVehicleSimpleFilter();
        public LicensePlateFilter LicensePlateFilter { get; set; } = new LicensePlateFilter();

        public string GetFilterParameters()
        { 
            return string.Join('&',
                MotorVehicleSimpleFilter.GetFilterParameters(), 
                LicensePlateFilter.GetFilterParameters());
        }
    }
}