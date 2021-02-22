using FleetManagement.ReadModels;
using System;

namespace FleetManagement.API.Read.Mappings
{
    public record LicensePlateSnapshotResponse
    (
        bool InUse,
        DateTime SnapshotDate,
        MotorVehicleSimpleResponse MotorVehicle
    );
}
