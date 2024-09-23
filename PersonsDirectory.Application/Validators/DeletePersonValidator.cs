using FluentValidation;
using PersonsDirectory.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Validators
{
    public class DeletePersonValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Person ID must be a positive integer.");
        }
    }
}
