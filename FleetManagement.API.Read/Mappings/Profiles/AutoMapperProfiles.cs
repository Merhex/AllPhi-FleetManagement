using AutoMapper;
using FleetManagement.Models;
using FleetManagement.ReadModels;

namespace FleetManagement.API.Read.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MotorVehicleLicensePlate, MotorVehicleResponse>();
            CreateMap<LicensePlate, LicensePlateResponse>();
            CreateMap<MotorVehicleDetailed, MotorVehicleDetailedResponse>();
            CreateMap<MotorVehicleMileageSnapshot, MotorVehicleMileageResponse>();
            CreateMap<MotorVehicleWorkOrder, MotorVehicleWorkOrderResponse>();
            CreateMap<Driver, DriverResponse>();
        }
    }
}
