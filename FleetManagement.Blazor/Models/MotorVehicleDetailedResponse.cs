using System.Collections.Generic;

namespace FleetManagement.Blazor.Models
{
    public record MotorVehicleDetailedResponse
    (
        string ChassisNumber,
        string Brand,
        string Model,
        bool Operational,
        int BodyType,
        int PropulsionType,
        DriverResponse Driver,
        IEnumerable<LicensePlateResponse> LicensePlates,
        IEnumerable<MotorVehicleMileageResponse> MileageHistory,
        IEnumerable<MotorVehicleWorkOrderResponse> Condition
    );
}
