using AutoMapper;
using FleetManagement.Models;
using FleetManagement.Models.ReadModels;

namespace FleetManagement.API.Read.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MotorVehicleLicensePlates, MotorVehicleResponse>();
            CreateMap<LicensePlate, LicensePlateResponse>();
        }
    }
}
