using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Mappings
{
    public class DriverResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }

        public DriverLicenseResponse DriverLicense { get; set; }
    }
}
