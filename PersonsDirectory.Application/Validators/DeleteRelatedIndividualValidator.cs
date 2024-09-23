using FluentValidation;
using PersonsDirectory.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Validators
{
    public class DeleteRelatedIndividualValidator : AbstractValidator<DeleteRelatedIndividualCommand>
    {
        public DeleteRelatedIndividualValidator()
        {
            RuleFor(x => x.PersonId).GreaterThan(0);
            RuleFor(x => x.RelatedPersonId).GreaterThan(0);
        }
    }
}
