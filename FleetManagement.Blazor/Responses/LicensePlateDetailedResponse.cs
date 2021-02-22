using System.Collections.Generic;

namespace FleetManagement.Blazor.Responses
{
    public class LicensePlateDetailedResponse
    {
        public string Identifier { get; set; }
        public bool InUse { get; set; }
        public List<LicensePlateSnapshotResponse> History { get; set; }
        public MotorVehicleSimpleResponse CurrentMotorVehicle { get; set; }
    }
}
