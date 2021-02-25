using FleetManagement.Models;
using FluentNHibernate.Mapping;

namespace FleetManagement.DAL.NHibernate.Mappings
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("Persons");

            Id(x => x.Id);

            Map(x => x.AddressLine)
                .Length(100)
                .Not.Nullable();

            Map(x => x.FirstName)
                .Length(50)
                .Not.Nullable();

            Map(x => x.LastName)
                .Length(50)
                .Not.Nullable();

            Map(x => x.DateOfBirth)
                .Not.Nullable();

            Map(x => x.NationalNumber)
                .CustomSqlType("nchar(21)")
                .Length(21)
                .Unique()
                .Not.Nullable();

            Map(x => x.ZipCode)
                .Not.Nullable();

            Map(x => x.City)
                .Length(50)
                .Not.Nullable();

            DiscriminateSubClassesOnColumn("Discriminator");
        }
    }
}
