using FleetManagement.DAL.NHibernate.Mappings;
using FleetManagement.Models;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace FleetManagement.DAL.NHibernate
{
    public static class ServiceCollectionsExtensions
    {
		public static IServiceCollection AddNHibernate(this IServiceCollection collection, string connectionString)
        {
            var configuration = new NamespaceMappingConfiguration();

            var sessionFactory = Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(x =>
                {
                    x.FluentMappings.Conventions.Add(DefaultLazy.Never());
                    x.FluentMappings.Add(typeof(DriverMap));
                    x.FluentMappings.Add(typeof(PersonMap));
                })
                .BuildSessionFactory();

            collection.AddSingleton(sessionFactory);
            collection.AddTransient(factory => sessionFactory.OpenSession());
            collection.AddMapperSessions();

            return collection;
        }

        private static IServiceCollection AddMapperSessions(this IServiceCollection collection)
        {
            collection.AddTransient<IDriverSession, DriverSession>();

            return collection;
        }
    }
}
