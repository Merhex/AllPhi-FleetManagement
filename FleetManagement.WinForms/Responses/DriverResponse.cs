using System;

namespace FleetManagement.WinForms.Responses
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
    );
}
