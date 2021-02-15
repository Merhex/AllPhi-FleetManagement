using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public interface IMapperSession
    {
        void BeginTransaction();
        Task Commit(CancellationToken cancellationToken = default);
        Task Rollback(CancellationToken cancellationToken = default);
        void CloseTransaction();
        Task Save<T>(T entity, CancellationToken cancellationToken = default);
        Task Delete<T>(T entity, CancellationToken cancellationToken = default);
    }
}
