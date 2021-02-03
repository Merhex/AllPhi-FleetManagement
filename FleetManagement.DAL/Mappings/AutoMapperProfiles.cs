using AutoMapper;
using FleetManagement.Models;
using FleetManagement.Models.ReadModels;
using System.Linq;

namespace FleetManagement.DAL.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MotorVehicle, MotorVehicleLicensePlates>()
                .ForMember(target => target.CurrentMileage, source => source
                .MapFrom(x => x.MileageHistory.LastOrDefault().Mileage));
        }
    }
}
