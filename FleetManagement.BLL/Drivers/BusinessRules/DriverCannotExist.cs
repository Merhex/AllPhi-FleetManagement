using FleetManagement.DAL.NHibernate;
using FleetManagement.Models;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.BusinessRules
{
    public class DriverCannotExist : IBusinessRule
    {
        private readonly string _nationalNumber;
        private readonly IDriverSession _driverSession;

        public DriverCannotExist(IDriverSession driverSession, string nationalNumber)
        {
            _nationalNumber = nationalNumber;
            _driverSession = driverSession;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var driver = await _driverSession.Drivers.SingleOrDefaultAsync(x => x.NationalNumber == _nationalNumber, cancellationToken);

            if (driver is not null)
                return new BusinessRuleResponse().Failure(this, $"The driver with national number: {_nationalNumber}, already exists.");
            else
                return BusinessRuleResponse.Success;
        }
    }
}
