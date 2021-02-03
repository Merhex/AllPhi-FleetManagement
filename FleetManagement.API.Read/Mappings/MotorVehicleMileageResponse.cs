using System;

namespace FleetManagement.API.Read.Mappings
{
    public record MotorVehicleMileageResponse
    (
        DateTime SnapshotDate,
        int Mileage
    );
}
