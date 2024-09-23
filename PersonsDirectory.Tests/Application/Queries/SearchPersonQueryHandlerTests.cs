using Moq;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Application.Queries.Handlers;
using PersonsDirectory.Application.Queries;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Tests.Application.Queries
{
    [TestFixture]
    public class SearchPersonQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private SearchPersonQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new SearchPersonQueryHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnSearchResults()
        {
            // Arrange
            var query = new SearchPersonQuery
            {
                SearchTerm = "John",
                City = City.NewYork,
                DateOfBirth = new DateTime(1990, 1, 1),
                PageNumber = 1,
                PageSize = 10
            };

            var persons = new List<Person>
            {
                new Person { Name = "John", Surname = "Doe", City = City.NewYork }
            };

            var searchResult = new SearchPersonResult
            {
                Results = persons,
                TotalCount = persons.Count
            };

            _unitOfWorkMock.Setup(u => u.Persons.SearchPersonAsync(query)).ReturnsAsync(searchResult);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Results, Is.EqualTo(searchResult.Results));
            Assert.That(result.TotalCount, Is.EqualTo(searchResult.TotalCount));
            _unitOfWorkMock.Verify(u => u.Persons.SearchPersonAsync(query), Times.Once);
        }
    }
}
