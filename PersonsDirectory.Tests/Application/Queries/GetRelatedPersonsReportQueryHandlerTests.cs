using Moq;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Application.Queries.Handlers;
using PersonsDirectory.Application.Queries;
using PersonsDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Tests.Application.Queries
{
    [TestFixture]
    public class GetRelatedPersonsReportQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private GetRelatedPersonsReportQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetRelatedPersonsReportQueryHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnRelatedPersonsReport()
        {
            var personId = 1;
            var report = new Dictionary<RelationshipType, int>
            {
                { RelationshipType.Colleague, 2 },
                { RelationshipType.Acquaintance, 3 }
            };

            _unitOfWorkMock.Setup(u => u.Persons.GetRelatedPersonsReportAsync(personId)).ReturnsAsync(report);

            var query = new GetRelatedPersonsReportQuery { PersonId = personId };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(report));
            _unitOfWorkMock.Verify(u => u.Persons.GetRelatedPersonsReportAsync(personId), Times.Once);
        }
    }
}
