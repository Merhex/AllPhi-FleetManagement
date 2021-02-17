using FleetManagement.Models;
using FluentNHibernate.Mapping;

namespace FleetManagement.DAL.NHibernate.Mappings
{
    public class DriverMap : SubclassMap<Driver>
    {
        public DriverMap()
        {
            Map(x => x.Active)
                .Not.Nullable();
        }
    }
}
