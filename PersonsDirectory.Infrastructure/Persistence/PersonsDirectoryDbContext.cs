using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Infrastructure.Persistence
{
    public class PersonsDirectoryDbContext : DbContext
    {
        public PersonsDirectoryDbContext(DbContextOptions<PersonsDirectoryDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<RelatedIndividual> RelatedIndividuals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
               .HasMany(p => p.PhoneNumbers)
               .WithOne()
               .HasForeignKey("PersonId");

            modelBuilder.Entity<Person>()
                .HasMany(p => p.RelatedIndividuals)
                .WithOne()
                .HasForeignKey("PersonId");

            modelBuilder.Entity<Person>()
                .Property(p => p.City)
                .HasConversion<int>();
        }
    }
}
