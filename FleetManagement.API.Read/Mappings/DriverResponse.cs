using System;

namespace FleetManagement.API.Read.Mappings
{
    public record DriverResponse
    (
        string NationalNumber,
        bool Active,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string AddressLine,
        string City,
        int ZipCode
        //DriverLicenseResponse DriverLicenseResponse
    );
}
