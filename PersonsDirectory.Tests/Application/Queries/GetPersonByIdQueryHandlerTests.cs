using Moq;
using PersonsDirectory.Application.Queries;
using PersonsDirectory.Application.Queries.Handlers;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.ValueObjects;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Enums;

[TestFixture]
public class GetPersonByIdQueryHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private GetPersonByIdQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new GetPersonByIdQueryHandler(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnPerson_WhenPersonExists()
    {
        var query = new GetPersonByIdQuery { Id = 1 };

        var person = new Person
        {
            Id = 1,
            Name = "John",
            Surname = "Doe",
            Gender = Gender.Male,
            PersonalNumber = "123456789",
            DateOfBirth = new DateTime(1990, 1, 1),
            City = City.NewYork,
            PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = "1234567890", Type = PhoneNumberType.Mobile } },
            ImagePath = "/images/johndoe.jpg",
            RelatedIndividuals = new List<RelatedIndividual>()
        };

        _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(query.Id))
            .ReturnsAsync(person);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.That(result.Person, Is.EqualTo(person));
        Assert.That(result.Person.Name, Is.EqualTo("John"));
        Assert.That(result.Person.Surname, Is.EqualTo("Doe"));
        Assert.That(result.Person.Gender, Is.EqualTo(Gender.Male));
        Assert.That(result.Person.ImagePath, Is.EqualTo("/images/johndoe.jpg"));
        Assert.That(result.RelatedIndividuals, Is.Not.Null);
        Assert.That(result.ImagePath, Is.EqualTo("/images/johndoe.jpg"));

        _unitOfWorkMock.Verify(u => u.Persons.GetByIdAsync(query.Id), Times.Once);
    }

    [Test]
    public void Handle_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
    {
        var query = new GetPersonByIdQuery { Id = 1 };

        _unitOfWorkMock.Setup(u => u.Persons.GetByIdAsync(query.Id))
            .ReturnsAsync((Person)null);

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            await _handler.Handle(query, CancellationToken.None));

        Assert.That(exception.Message, Is.EqualTo("Person with ID 1 not found."));

        _unitOfWorkMock.Verify(u => u.Persons.GetByIdAsync(query.Id), Times.Once);
    }
}
