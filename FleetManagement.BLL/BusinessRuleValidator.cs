using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.BLL
{
    public class BusinessRuleValidator : IBusinessRuleValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public BusinessRuleValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IBusinessRuleListenerResponse> Validate<T>(T contract, CancellationToken token = default) where T : IContract
        {
            var businessRuleValidator = _serviceProvider.GetService<IBusinessRuleValidator<T>>();

            return await businessRuleValidator.Validate(contract, token);
        }
    }
}
