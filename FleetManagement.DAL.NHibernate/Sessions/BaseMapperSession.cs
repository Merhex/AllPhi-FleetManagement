using NHibernate;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public abstract class BaseMapperSession : IMapperSession
    {
        protected readonly ISession _session;
        protected ITransaction _transaction;

        public BaseMapperSession(ISession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task Rollback(CancellationToken cancellation = default)
        {
            await _transaction.RollbackAsync(cancellation);
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task Save<T>(T entity, CancellationToken cancellationToken = default)
        {
            await _session.SaveOrUpdateAsync(entity, cancellationToken);
        }

        public async Task Delete<T>(T entity, CancellationToken cancellationToken = default)
        {
            await _session.DeleteAsync(entity, cancellationToken);
        }
    }
}
