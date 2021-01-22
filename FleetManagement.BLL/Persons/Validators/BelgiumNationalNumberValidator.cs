using FleetManagement.BLL.Persons.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.Validators
{
    public class BelgiumNationalNumberValidator : IBelgianNationalNumberValidator
    {
        public bool ValidateNationalNumberChecksum(string nationalNumber, DateTime birthDate) =>
            BelgianNationalNumberChecker.ValidateChecksum(birthDate, nationalNumber);

        public bool ValidateNationalNumberForContainingBirthdate(string nationalNumber, DateTime birthDate) =>
            BelgianNationalNumberChecker.ValidateForContainingBirthdate(birthDate, nationalNumber);

        public bool ValidateNationalNumberFormatting(string nationalNumber) =>
            BelgianNationalNumberChecker.ValidateFormatting(nationalNumber);
    }
}
