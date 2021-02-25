using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .HasKey(person => person.Id);

            builder
                .Property(u => u.FirstName)
                .HasMaxLength(50); 
            
            builder
                .Property(u => u.LastName)
                .HasMaxLength(50);

            builder
                .Property(u => u.City)
                .HasMaxLength(50);

            builder
                .Property(u => u.AddressLine)
                .HasMaxLength(100);

            builder
                .Property(u => u.NationalNumber)
                .HasMaxLength(21)
                .IsRequired()
                .IsFixedLength();
        }
    }
}
