using Moq;
using PersonsDirectory.Application.Commands.Handlers;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Tests.Application.Commands
{
    [TestFixture]
    public class UpdatePersonCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private UpdatePersonCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdatePersonCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldUpdatePerson_WhenPersonExists()
        {
            var command = new UpdatePersonCommand
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Gender = Gender.Male,
                PersonalNumber = "123456789",
                DateOfBirth = new DateTime(1990, 1, 1),
                City = City.NewYork,
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = "1234567890", Type = PhoneNumberType.Mobile } },
                ImagePath = "/images/johndoe.jpg"
            };

            var person = new Person
            {
                Id = 1,
                Name = "Initial Name",
                Surname = "Initial Surname",
                Gender = Gender.Female,  
                PersonalNumber = "000000000",
                DateOfBirth = new DateTime(1980, 1, 1),
                City = City.Paris,
                PhoneNumbers = new List<PhoneNumber>(),
                RelatedIndividuals = new List<RelatedIndividual>()
            };

            person.PhoneNumbers = new List<PhoneNumber>();

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(command.Id))
                .ReturnsAsync(person);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(person.Name, Is.EqualTo(command.Name));
            Assert.That(person.Surname, Is.EqualTo(command.Surname));
            Assert.That(person.Gender, Is.EqualTo(command.Gender));
            Assert.That(person.PersonalNumber, Is.EqualTo(command.PersonalNumber));
            Assert.That(person.DateOfBirth, Is.EqualTo(command.DateOfBirth));
            Assert.That(person.City, Is.EqualTo(command.City));
            Assert.That(person.ImagePath, Is.EqualTo(command.ImagePath));
            Assert.That(person.PhoneNumbers.Count, Is.EqualTo(1));
            Assert.That(person.PhoneNumbers.First().Number, Is.EqualTo("1234567890"));
            Assert.That(person.PhoneNumbers.First().Type, Is.EqualTo(PhoneNumberType.Mobile));

            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(Unit.Value));
        }

        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
        {
            var command = new UpdatePersonCommand { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(command.Id))
                .ReturnsAsync((Person)null); 

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo("Person with ID 1 not found."));
        }
    }

}
