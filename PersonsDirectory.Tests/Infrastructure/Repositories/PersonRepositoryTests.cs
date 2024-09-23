using Microsoft.EntityFrameworkCore;
using Moq;
using PersonsDirectory.Application.Queries;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Infrastructure.Persistence;
using PersonsDirectory.Infrastructure.Repositories;

namespace PersonsDirectory.Tests.Infrastructure.Repositories
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private PersonsDirectoryDbContext _dbContext;
        private PersonRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PersonsDirectoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new PersonsDirectoryDbContext(options);
            _repository = new PersonRepository(_dbContext);

            SeedTestData();
        }

        private void SeedTestData()
        {
            var persons = new List<Person>
        {
            new Person { Name = "John", Surname = "Doe", PersonalNumber = "123456789", City = City.NewYork },
            new Person { Name = "Jane", Surname = "Smith", PersonalNumber = "987654321", City = City.Berlin }
        };
            _dbContext.Persons.AddRange(persons);
            _dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task SearchPersonAsync_ShouldFilterBySearchTerm()
        {
            var query = new SearchPersonQuery { SearchTerm = "John" };

            var result = await _repository.SearchPersonAsync(query);

            Assert.That(result.Results.First().Name, Is.EqualTo("John"));
        }

        [Test]
        public async Task SearchPersonAsync_ShouldFilterByCity()
        {
            var query = new SearchPersonQuery { City = City.NewYork };

            var result = await _repository.SearchPersonAsync(query);

            Assert.That(result.Results.Count(), Is.EqualTo(1));
            Assert.That(result.Results.First().City, Is.EqualTo(City.NewYork));
        }
    }


}

