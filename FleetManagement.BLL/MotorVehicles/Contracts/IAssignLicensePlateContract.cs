using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Contracts
{
    public interface IAssignLicensePlateContract
    {
        public int MotorVehicleId { get; init; }
        public int LicensePlateId { get; set; }
    }
}
