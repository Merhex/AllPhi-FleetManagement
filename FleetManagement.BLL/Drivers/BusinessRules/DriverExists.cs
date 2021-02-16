using FleetManagement.DAL.NHibernate;
using NHibernate.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.BusinessRules
{
    public class DriverExists : IBusinessRule
    {
        private readonly string _nationalNumber;
        private readonly IDriverSession _driverSession;

        public DriverExists(IDriverSession driverSession, string nationalNumber)
        {
            _nationalNumber = nationalNumber;
            _driverSession = driverSession;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var driver = await _driverSession.GetDriverByNationalNumber(_nationalNumber, cancellationToken);

            if (driver is null)
                return new BusinessRuleResponse().Failure(this, $"The driver with national number: {_nationalNumber}, does not exist.");
            else
                return BusinessRuleResponse.Success;
        }
    }
}
