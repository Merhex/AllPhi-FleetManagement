using System;

namespace FleetManagement.Blazor.Responses
{
    public class LicensePlateSnapshotResponse
    {
        public bool InUse { get; set; }
        public DateTime SnapshotDate {get;set;}
        public MotorVehicleSimpleResponse MotorVehicle {get;set;}
        public string LicensePlateIdentifier {get;set;}
    }
}
