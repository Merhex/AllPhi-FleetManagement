using System.Collections.Generic;

namespace FleetManagement.Blazor.Filters
{
    public class LicensePlateHistoryFilter
    {
        public MotorVehicleSimpleFilter MotorVehicleSimpleFilter { get; set; } = new MotorVehicleSimpleFilter();
        public LicensePlateFilter LicensePlateFilter { get; set; } = new LicensePlateFilter();

        public string GetFilterParameters()
        {
            var motorVehicleParams = MotorVehicleSimpleFilter.GetFilterParameters();
            var licensePlateParams = LicensePlateFilter.GetFilterParameters();

            return string.Join('&', motorVehicleParams, licensePlateParams);
        }
    }
}