using System.Collections.Generic;

namespace FleetManagement.Blazor.Responses
{
    public class LicensePlateDetailedResponse
    {
        public string Identifier { get; set; }
        public bool InUse { get; set; }
        public IEnumerable<LicensePlateSnapshotResponse> History { get; set; }
        public MotorVehicleSimpleResponse CurrentMotorVehicle { get; set; }
    }
}
