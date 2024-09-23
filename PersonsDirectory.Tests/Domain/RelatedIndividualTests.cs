using FluentValidation.TestHelper;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Validators;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Tests.Domain
{
    [TestFixture]
    public class RelatedIndividualTests
    {
        [Test]
        public void CreateRelatedIndividual_ShouldInitializeCorrectly()
        {
            int relatedPersonId = 1;
            RelationshipType relationshipType = RelationshipType.Acquaintance;

            var relatedIndividual = new RelatedIndividual
            {
                RelatedPersonId = relatedPersonId,
                RelationshipType = relationshipType
            };

            Assert.That(relatedIndividual.RelatedPersonId, Is.EqualTo(relatedPersonId));
            Assert.That(relatedIndividual.RelationshipType, Is.EqualTo(relationshipType));
        }

        [Test]
        public void SetRelatedPersonId_ShouldUpdateCorrectly()
        {
            var relatedIndividual = new RelatedIndividual { RelatedPersonId = 1 };
            int newRelatedPersonId = 2;

            relatedIndividual.RelatedPersonId = newRelatedPersonId;

            Assert.That(relatedIndividual.RelatedPersonId, Is.EqualTo(newRelatedPersonId));
        }

        [Test]
        public void SetRelationshipType_ShouldUpdateCorrectly()
        {
            var relatedIndividual = new RelatedIndividual { RelationshipType = RelationshipType.Relative };
            RelationshipType newRelationshipType = RelationshipType.Colleague;

            relatedIndividual.RelationshipType = newRelationshipType;

            Assert.That(relatedIndividual.RelationshipType, Is.EqualTo(newRelationshipType));
        }

        [Test]
        public void RelatedIndividual_ShouldThrowValidationError_WhenRelatedPersonIdIsInvalid()
        {

            var validator = new CreateRelatedIndividualValidator();
            var command = new CreateRelatedIndividualCommand
            {
                PersonId = 1,
                RelatedPersonId = -1, 
                RelationshipType = RelationshipType.Relative
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.RelatedPersonId);
        }

    }
}
