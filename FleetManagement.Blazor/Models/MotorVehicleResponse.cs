using System.Collections.Generic;

namespace FleetManagement.Blazor.Models
{
    public record MotorVehicleResponse
    (
        string ChassisNumber,
        string Brand,
        string Model,
        bool Operational,
        int CurrentMileage,
        int BodyType,
        int PropulsionType,
        string LicensePlateIdentifier
    );
}
