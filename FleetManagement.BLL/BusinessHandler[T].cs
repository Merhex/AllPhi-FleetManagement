using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class BusinessHandler<T> : IBusinessHandler<T> where T : IContract
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBusinessRequirements<T> _requirements;

        public BusinessHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _requirements = _serviceProvider.GetRequiredService<IBusinessRequirements<T>>();
        }

        public async Task<IBusinessHandlerResponse> Validate(T contract, CancellationToken cancellationToken = default)
        {
            _requirements.Read(contract);

            if (_requirements.BusinessRules.Count is 0) 
                throw new Exception("There are no business rules defined in the requirement.");

            var businessHandlerResponse = new BusinessHandlerResponse();

            foreach (var businessRule in _requirements.BusinessRules)
            {
                var response = await businessRule.Validate(cancellationToken);

                businessHandlerResponse.Responses.Add(response);
            }

            return businessHandlerResponse;
        }
    }
}
