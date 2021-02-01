using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IAssignLicensePlateContract : IContract
    {
        public string Identifier { get; init; }
        public string ChassisNumber { get; init; }
    }
}
