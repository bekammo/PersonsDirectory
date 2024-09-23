using Moq;
using PersonsDirectory.Application.Commands.Handlers;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace PersonsDirectory.Tests.Application.Commands
{

    [TestFixture]
    public class DeletePersonCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private DeletePersonCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeletePersonCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldRemovePerson_WhenPersonExists()
        {
            var command = new DeletePersonCommand { Id = 1 };
            var person = new Person { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(command.Id))
                .ReturnsAsync(person);

            var result = await _handler.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(u => u.Persons.Remove(It.IsAny<Person>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(Unit.Value));
        }

        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
        {
            var command = new DeletePersonCommand { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(command.Id))
                .ReturnsAsync((Person)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo("Person with ID 1 not found."));
        }
    }

}
