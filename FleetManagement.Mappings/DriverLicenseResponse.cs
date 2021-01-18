using FleetManagement.Models;
using System;
using System.Collections.Generic;

namespace FleetManagement.Mappings
{
    public class DriverLicenseResponse
    {
        public string NameHolder { get; set; }
        public string Identifier { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Categories { get; set; }
    }
}
