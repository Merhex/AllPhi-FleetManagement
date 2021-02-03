using System.Collections.Generic;

namespace FleetManagement.API.Read.Mappings
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
        LicensePlateResponse LicensePlate
    );
}
