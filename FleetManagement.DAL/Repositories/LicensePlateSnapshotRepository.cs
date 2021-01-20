using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;

namespace FleetManagement.DAL.Repositories
{
    public class LicensePlateSnapshotRepository : Repository<LicensePlateSnapshot, int>, ILicensePlateSnaphotRepository
    {
        public LicensePlateSnapshotRepository(FleetManagementContext context) : base(context) { }
    }
}
