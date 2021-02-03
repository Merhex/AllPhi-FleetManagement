using FleetManagement.API.Read.Mappings;
using MediatR;

namespace FleetManagement.API.Read.Queries
{
    public class MotorVehicleDetailedQuery : IRequest<MotorVehicleDetailedResponse>
    {
        public string ChassisNumber { get; set; }
    }
}
