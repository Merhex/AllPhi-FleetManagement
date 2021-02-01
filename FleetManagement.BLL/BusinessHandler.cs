using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.BLL
{
    public class BusinessHandler : IBusinessHandler
    {
        private readonly IServiceProvider _provider;

        public BusinessHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<IBusinessHandlerResponse> Validate<T>(T contract, CancellationToken cancellationToken = default) where T : IContract
        {
            var handler = _provider.GetService<IBusinessHandler<T>>();

            return await handler.Validate(contract, cancellationToken);
        }
    }
}
