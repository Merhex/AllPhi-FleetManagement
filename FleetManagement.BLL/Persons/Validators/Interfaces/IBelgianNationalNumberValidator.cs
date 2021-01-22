using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.Validators.Interfaces
{
    public interface IBelgianNationalNumberValidator
    {
        bool ValidateNationalNumberFormatting(string nationalNumber);
        bool ValidateNationalNumberForContainingBirthdate(string nationalNumber, DateTime birthDate);
        bool ValidateNationalNumberChecksum(string nationalNumber, DateTime birthDate);
    }
}
