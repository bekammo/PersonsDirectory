using Moq;
using PersonsDirectory.Application.Commands.Handlers;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Entities;
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
    public class DeleteRelatedIndividualCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private DeleteRelatedIndividualCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteRelatedIndividualCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldRemoveRelatedIndividual_WhenPersonAndRelatedIndividualExist()
        {
            var command = new DeleteRelatedIndividualCommand { PersonId = 1, RelatedPersonId = 2 };
            var relatedIndividual = new RelatedIndividual { RelatedPersonId = 2 };
            var person = new Person
            {
                Id = 1,
                RelatedIndividuals = new List<RelatedIndividual> { relatedIndividual }
            };

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(command.PersonId))
                .ReturnsAsync(person);

            var result = await _handler.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(Unit.Value));
            Assert.That(person.RelatedIndividuals.Count, Is.EqualTo(0));
        }

        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
        {
            var command = new DeleteRelatedIndividualCommand { PersonId = 1, RelatedPersonId = 2 };

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(command.PersonId))
                .ReturnsAsync((Person)null); 

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo("Person with ID 1 not found."));
        }

        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenRelatedIndividualDoesNotExist()
        {
            var command = new DeleteRelatedIndividualCommand { PersonId = 1, RelatedPersonId = 2 };
            var person = new Person
            {
                Id = 1,
                RelatedIndividuals = new List<RelatedIndividual>()
            };

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(command.PersonId))
                .ReturnsAsync(person);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo("Related individual with ID 2 not found for person with ID 1."));
        }
    }

}
