using FleetManagement.DAL.NHibernate.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace FleetManagement.DAL.NHibernate
{
    public static class ServiceCollectionsExtensions
    {
		public static IServiceCollection AddNHibernate(this IServiceCollection collection, string connectionString)
        {
            var configuration = new Configuration().DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.ConnectionString = connectionString;
                db.BatchSize = 100;
                db.Driver<SqlClientDriver>();
                db.SchemaAction = SchemaAutoAction.Validate;
            });

            var sessionFactory = Fluently
                .Configure(configuration)
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
