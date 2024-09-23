using Microsoft.EntityFrameworkCore;
using Moq;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Infrastructure.Persistence;
using PersonsDirectory.Infrastructure.Repositories;

namespace PersonsDirectory.Tests.Infrastructure.Repositories
{
    [TestFixture]
    public class RepositoryTests
    {
        private Mock<PersonsDirectoryDbContext> _dbContextMock;
        private Mock<DbSet<Person>> _dbSetMock;
        private Repository<Person> _repository;

        [SetUp]
        public void SetUp()
        {
            _dbSetMock = new Mock<DbSet<Person>>();
            _dbContextMock = new Mock<PersonsDirectoryDbContext>(new DbContextOptions<PersonsDirectoryDbContext>());
            _dbContextMock.Setup(c => c.Set<Person>()).Returns(_dbSetMock.Object);

            _repository = new Repository<Person>(_dbContextMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            var person = new Person { Id = 1, Name = "John" };
            _dbSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(person);

            var result = await _repository.GetByIdAsync(1);

            Assert.That(result, Is.EqualTo(person));
        }

        [Test]
        public async Task AddAsync_ShouldAddEntity()
        {
            var person = new Person { Id = 1, Name = "John" };

            await _repository.AddAsync(person);

            _dbSetMock.Verify(m => m.AddAsync(person, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Remove_ShouldRemoveEntity()
        {
            var person = new Person { Id = 1, Name = "John" };

            _repository.Remove(person);

            _dbSetMock.Verify(m => m.Remove(person), Times.Once);
        }

        [Test]
        public void Update_ShouldUpdateEntity()
        {
            var person = new Person { Id = 1, Name = "John" };

            _repository.Update(person);

            _dbSetMock.Verify(m => m.Update(person), Times.Once);
        }
    }

}
