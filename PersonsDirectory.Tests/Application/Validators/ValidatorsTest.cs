using FluentValidation.TestHelper;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Validators;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Tests.Application.Validators
{
    [TestFixture]
    public class ValidatorsTests
    {
        private CreatePersonValidator _createPersonValidator;
        private CreateRelatedIndividualValidator _createRelatedIndividualValidator;
        private DeletePersonValidator _deletePersonValidator;
        private DeleteRelatedIndividualValidator _deleteRelatedIndividualValidator;
        private PhoneNumberValidator _phoneNumberValidator;
        private RelatedIndividualValidator _relatedIndividualValidator;
        private UpdatePersonValidator _updatePersonValidator;

        [SetUp]
        public void Setup()
        {
            _createPersonValidator = new CreatePersonValidator();
            _createRelatedIndividualValidator = new CreateRelatedIndividualValidator();
            _deletePersonValidator = new DeletePersonValidator();
            _deleteRelatedIndividualValidator = new DeleteRelatedIndividualValidator();
            _phoneNumberValidator = new PhoneNumberValidator();
            _relatedIndividualValidator = new RelatedIndividualValidator();
            _updatePersonValidator = new UpdatePersonValidator();
        }

        #region CreatePersonValidator Tests

        [Test]
        public void CreatePerson_ShouldHaveError_WhenNameIsNull()
        {
            var command = new CreatePersonCommand { Name = string.Empty };
            var result = _createPersonValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void CreatePerson_ShouldNotHaveError_WhenValid()
        {
            var command = new CreatePersonCommand
            {
                Name = "John",
                Surname = "Doe",
                Gender = Gender.Male,
                PersonalNumber = "12345678910",
                DateOfBirth = new DateTime(1990, 1, 1),
                City = City.NewYork,
                PhoneNumbers = new List<PhoneNumber>(),
                ImagePath = "path/to/image.jpg",
                RelatedIndividuals = new List<RelatedIndividual>()
            };
            var result = _createPersonValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion

        #region CreateRelatedIndividualValidator Tests

        [Test]
        public void CreateRelatedIndividual_ShouldHaveError_WhenPersonIdIsZero()
        {
            var command = new CreateRelatedIndividualCommand { PersonId = 0 };
            var result = _createRelatedIndividualValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PersonId);
        }

        [Test]
        public void CreateRelatedIndividual_ShouldNotHaveError_WhenValid()
        {
            var command = new CreateRelatedIndividualCommand
            {
                PersonId = 1,
                RelatedPersonId = 2,
                RelationshipType = RelationshipType.Colleague
            };
            var result = _createRelatedIndividualValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion

        #region DeletePersonValidator Tests

        [Test]
        public void DeletePerson_ShouldHaveError_WhenIdIsZero()
        {
            var command = new DeletePersonCommand { Id = 0 };
            var result = _deletePersonValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Test]
        public void DeletePerson_ShouldNotHaveError_WhenValid()
        {
            var command = new DeletePersonCommand { Id = 1 };
            var result = _deletePersonValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion

        #region DeleteRelatedIndividualValidator Tests

        [Test]
        public void DeleteRelatedIndividual_ShouldHaveError_WhenPersonIdIsZero()
        {
            var command = new DeleteRelatedIndividualCommand { PersonId = 0 };
            var result = _deleteRelatedIndividualValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PersonId);
        }

        [Test]
        public void DeleteRelatedIndividual_ShouldNotHaveError_WhenValid()
        {
            var command = new DeleteRelatedIndividualCommand { PersonId = 1, RelatedPersonId = 2 };
            var result = _deleteRelatedIndividualValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion

        #region PhoneNumberValidator Tests

        [Test]
        public void PhoneNumber_ShouldHaveError_WhenNumberIsNull()
        {
            var phoneNumber = new PhoneNumber { Number = string.Empty };
            var result = _phoneNumberValidator.TestValidate(phoneNumber);
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Test]
        public void PhoneNumber_ShouldNotHaveError_WhenValid()
        {
            var phoneNumber = new PhoneNumber { Number = "12345678910", Type = PhoneNumberType.Office };
            var result = _phoneNumberValidator.TestValidate(phoneNumber);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion

        #region UpdatePersonValidator Tests

        [Test]
        public void UpdatePerson_ShouldHaveError_WhenNameIsNull()
        {
            var command = new UpdatePersonCommand { Name = string.Empty };
            var result = _updatePersonValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void UpdatePerson_ShouldNotHaveError_WhenValid()
        {
            var command = new UpdatePersonCommand
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Gender = Gender.Male,
                PersonalNumber = "12345678910",
                DateOfBirth = new DateTime(1990, 1, 1),
                City = City.NewYork,
                PhoneNumbers = new List<PhoneNumber>(),
                ImagePath = "path/to/image.jpg"
            };
            var result = _updatePersonValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }

}
