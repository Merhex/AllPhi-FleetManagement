using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IAssignLicensePlateContract
    {
        public string ChassisNumber { get; init; }
        public string LicensePlateIdentifier { get; set; }
    }
}
