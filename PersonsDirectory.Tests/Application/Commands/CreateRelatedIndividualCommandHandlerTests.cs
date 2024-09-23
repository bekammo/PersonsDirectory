using Moq;
using PersonsDirectory.Application.Commands.Handlers;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace PersonsDirectory.Tests.Application.Commands
{
    [TestFixture]
    public class CreateRelatedIndividualCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CreateRelatedIndividualCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateRelatedIndividualCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldAddRelatedIndividual_WhenPersonExists()
        {
            var command = new CreateRelatedIndividualCommand
            {
                PersonId = 1,
                RelatedPersonId = 2,
                RelationshipType = RelationshipType.Relative
            };

            var person = new Person { Id = 1, RelatedIndividuals = new List<RelatedIndividual>() };

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(person);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(person.RelatedIndividuals.Count, Is.EqualTo(1));
            _unitOfWorkMock.Verify(u => u.Persons.Update(person), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

    }

}
