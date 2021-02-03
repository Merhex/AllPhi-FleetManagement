using AutoMapper;
using FleetManagement.Models;
using FleetManagement.ReadModels;
using System.Linq;

namespace FleetManagement.DAL.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MotorVehicle, MotorVehicleLicensePlate>()
                .ForMember(target => target.CurrentMileage, source => source
                    .MapFrom(x => x.MileageHistory.FirstOrDefault().Mileage))
                .ForMember(target => target.LicensePlate, source => source
                    .MapFrom(x => x.LicensePlates.SingleOrDefault(x => x.InUse)));

            CreateMap<MotorVehicle, MotorVehicleDetailed>();
        }
    }
}
