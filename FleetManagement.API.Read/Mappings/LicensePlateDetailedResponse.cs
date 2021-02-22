using System.Collections.Generic;

namespace FleetManagement.API.Read.Mappings
{
    public record LicensePlateDetailedResponse
    (
        string Identifier,
        bool InUse,
        IEnumerable<LicensePlateSnapshotResponse> History,
        MotorVehicleSimpleResponse AssignedMotorVehicle
    );
}
