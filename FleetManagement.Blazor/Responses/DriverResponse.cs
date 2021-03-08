using System;

namespace FleetManagement.Blazor.Responses
{
    public class DriverResponse
    {
        public string NationalNumber { get; set; }
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}
