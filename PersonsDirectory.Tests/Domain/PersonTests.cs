using FluentValidation.TestHelper;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Validators;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Tests.Domain
{
    [TestFixture]
    public class PersonTests
    {
        [Test]
        public void Person_ShouldCreateWithValidProperties()
        {
            var phoneNumber = new PhoneNumber { Number = "1234567890", Type = PhoneNumberType.Mobile };
            var relatedIndividual = new RelatedIndividual { RelatedPersonId = 1, RelationshipType = RelationshipType.Other };

            var person = new Person
            {
                Name = "John",
                Surname = "Doe",
                Gender = Gender.Male,
                PersonalNumber = "123456789",
                DateOfBirth = new DateTime(1990, 1, 1),
                City = City.NewYork,
                PhoneNumbers = new List<PhoneNumber> { phoneNumber },
                RelatedIndividuals = new List<RelatedIndividual> { relatedIndividual },
                ImagePath = "/images/john.jpg"
            };

            Assert.That(person.Name, Is.EqualTo("John"));
            Assert.That(person.Surname, Is.EqualTo("Doe"));
            Assert.That(person.Gender, Is.EqualTo(Gender.Male));
            Assert.That(person.PersonalNumber, Is.EqualTo("123456789"));
            Assert.That(person.DateOfBirth, Is.EqualTo(new DateTime(1990, 1, 1)));
            Assert.That(person.City, Is.EqualTo(City.NewYork));
            Assert.That(person.PhoneNumbers, Is.Not.Empty);
            Assert.That(person.RelatedIndividuals, Is.Not.Empty);

        }

        [Test]
        public void Person_ShouldHaveErrors_WhenMissingRequiredFields()
        {
            var command = new CreatePersonCommand
            {
                Name = "",
                Surname = "",
                Gender = 0,
                PersonalNumber = "",
                DateOfBirth = DateTime.Today,
                PhoneNumbers = new List<PhoneNumber>(),
                RelatedIndividuals = new List<RelatedIndividual>()
            };

            var validator = new CreatePersonValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("Name is required.");

            result.ShouldHaveValidationErrorFor(x => x.Surname)
                  .WithErrorMessage("Surname is required.");

            result.ShouldHaveValidationErrorFor(x => x.Gender)
                  .WithErrorMessage("Gender must be either Male or Female.");

            result.ShouldHaveValidationErrorFor(x => x.PersonalNumber)
                  .WithErrorMessage("Personal number is required.");

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                  .WithErrorMessage("Person must be at least 18 years old.");
        }

    }
}
