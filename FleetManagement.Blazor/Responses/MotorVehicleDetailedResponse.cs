using System.Collections.Generic;

namespace FleetManagement.Blazor.Responses
{
    public class MotorVehicleDetailedResponse
    {
        public string ChassisNumber { get; set; }
        public string Brand {get;set;}
        public string Model {get;set;}
        public bool Operational {get;set;}
        public int BodyType {get;set;}
        public int PropulsionType {get;set;}
        public DriverResponse Driver {get;set;}
        public IEnumerable<LicensePlateResponse> LicensePlates {get;set;}
        public IEnumerable<MotorVehicleMileageResponse> MileageHistory {get;set;}
        public IEnumerable<MotorVehicleWorkOrderResponse> Conditio {get;set;}
    }
}
