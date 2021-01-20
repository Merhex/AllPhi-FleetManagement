using FleetManagement.Mappings;
using MediatR;

namespace FleetManagement.BLL.Drivers.Queries
{
    public class GetDriverByNationalNumberQuery : IRequest<DriverResponse>
    {
        public string NationalNumber { get; }

        public GetDriverByNationalNumberQuery(string nationalNumber)
        {
            NationalNumber = nationalNumber;
        }
    }
}
