using FluentValidation;
using PersonsDirectory.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Validators
{
    public class CreateRelatedIndividualValidator : AbstractValidator<CreateRelatedIndividualCommand>
    {
        public CreateRelatedIndividualValidator()
        {
            RuleFor(x => x.PersonId).GreaterThan(0);
            RuleFor(x => x.RelatedPersonId).GreaterThan(0);
            RuleFor(x => x.RelationshipType).IsInEnum();
        }
    }
}
