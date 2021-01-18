using AutoMapper;
using FleetManagement.Mappings;
using FleetManagement.Models;

namespace FleetManagement.API.Read.Mappings.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Driver, DriverResponse>();
            CreateMap<DriverLicense, DriverLicenseResponse>();
        }
    }
}
