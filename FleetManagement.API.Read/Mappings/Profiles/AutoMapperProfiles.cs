using AutoMapper;
using FleetManagement.Models;
using FleetManagement.ReadModels;

namespace FleetManagement.API.Read.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MotorVehicleLicensePlate, MotorVehicleResponse>()
                .ForMember(x => x.LicensePlateIdentifier, x => x
                    .MapFrom(x => x.LicensePlate.Identifier));

            CreateMap<LicensePlate, LicensePlateResponse>();

            CreateMap<MotorVehicleDetailed, MotorVehicleDetailedResponse>();

            CreateMap<MotorVehicleMileageSnapshot, MotorVehicleMileageResponse>();

            CreateMap<MotorVehicleWorkOrder, MotorVehicleWorkOrderResponse>();

            CreateMap<MotorVehicle, MotorVehicleSimpleResponse>();

            CreateMap<Driver, DriverResponse>();

            CreateMap<LicensePlateSnapshot, LicensePlateSnapshotResponse>();
        }
    }
}
