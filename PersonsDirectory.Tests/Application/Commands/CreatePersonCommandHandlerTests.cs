using Moq;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Application.Commands.Handlers;

namespace PersonsDirectory.Tests.Application.Commands
{
    [TestFixture]
    public class CreatePersonCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CreatePersonCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreatePersonCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldCreatePerson_WhenCommandIsValid()
        {
            var command = new CreatePersonCommand
            {
                Name = "John",
                Surname = "Doe",
                Gender = Gender.Male,
                PersonalNumber = "123456789",
                DateOfBirth = new System.DateTime(1990, 1, 1),
                City = City.NewYork
            };

            var cancellationToken = CancellationToken.None;

            _unitOfWorkMock.Setup(u => u.Persons.AddAsync(It.IsAny<Person>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _handler.Handle(command, cancellationToken);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Surname, Is.EqualTo(command.Surname));
            Assert.That(result.Gender, Is.EqualTo(command.Gender));
            Assert.That(result.PersonalNumber, Is.EqualTo(command.PersonalNumber));
            Assert.That(result.DateOfBirth, Is.EqualTo(command.DateOfBirth));
            Assert.That(result.City, Is.EqualTo(command.City));

            _unitOfWorkMock.Verify(u => u.Persons.AddAsync(It.IsAny<Person>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }

}
