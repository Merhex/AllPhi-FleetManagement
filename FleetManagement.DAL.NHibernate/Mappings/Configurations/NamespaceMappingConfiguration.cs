using FluentNHibernate;
using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate.Mappings
{
    public class NamespaceMappingConfiguration : DefaultAutomappingConfiguration
    {
        private readonly string _namespace = "FleetManagement.DAL.NHibernate.Mappings";

        public override bool ShouldMap(Type type)
        {
            return type.Namespace == _namespace;
        }
    }
}
