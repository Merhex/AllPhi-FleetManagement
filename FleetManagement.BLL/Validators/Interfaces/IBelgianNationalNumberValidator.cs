using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Validators.Interfaces
{
    public interface IBelgianNationalNumberValidator
    {
        bool ValidateFormat(string nationalNumber);
        bool Validate(DateTime birthdate, string nationalNumber);
    }
}
