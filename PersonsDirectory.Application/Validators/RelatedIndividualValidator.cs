using FluentValidation;
using PersonsDirectory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Validators
{
    public class RelatedIndividualValidator : AbstractValidator<RelatedIndividual>
    {
        public RelatedIndividualValidator()
        {
            RuleFor(individual => individual.RelationshipType)
                .IsInEnum().WithMessage("Relationship type must be Colleague, Acquaintance, Relative, or Other.");

            RuleFor(individual => individual.RelatedPersonId)
                .GreaterThan(0).WithMessage("Related person ID must be a positive integer.");
        }
    }
}
