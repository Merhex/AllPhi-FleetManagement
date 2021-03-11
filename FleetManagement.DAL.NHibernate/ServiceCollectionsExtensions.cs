using FleetManagement.DAL.NHibernate.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FleetManagement.DAL.NHibernate
{
    public static class ServiceCollectionsExtensions
    {
		public static IServiceCollection AddNHibernate(this IServiceCollection collection, string connectionString)
        {
            #if DEBUG
            Task.Delay(5000).Wait();
            #endif

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
            collection.AddTransient<IPersonSession, PersonSession>();
            collection.AddTransient<IReadDriverSession, ReadDriverSession>();

            return collection;
        }
    }
}
