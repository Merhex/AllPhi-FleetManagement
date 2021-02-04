using System;

namespace FleetManagement.Blazor.Models
{
    public record MotorVehicleMileageResponse
    (
        DateTime SnapshotDate,
        int Mileage
    );
}
