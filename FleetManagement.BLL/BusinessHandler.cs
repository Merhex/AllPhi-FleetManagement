using System;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class BusinessHandler : IBusinessHandler
    {
        private readonly IServiceProvider _provider;

        public BusinessHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<IBusinessHandlerResponse> Validate<T>(T contact, CancellationToken cancellationToken = default) where T : IContract
        {
            var handler = _provider.GetService<IBusinessHandler<T>>();

            handler.Read(contact);

            return await handler.Validate(cancellationToken);
        }
    }
}
