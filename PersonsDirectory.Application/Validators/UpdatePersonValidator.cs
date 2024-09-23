using FluentValidation;
using PersonsDirectory.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Validators
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[\p{IsGeorgian}a-zA-Z]+$").WithMessage("Name must contain only Georgian or Latin letters.");

            RuleFor(person => person.Surname)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[\p{IsGeorgian}a-zA-Z]+$").WithMessage("Surname must contain only Georgian or Latin letters.");

            RuleFor(person => person.PersonalNumber)
                .NotEmpty()
                .Matches(@"^\d{11}$").WithMessage("Personal number must be exactly 11 digits.");

            RuleFor(person => person.DateOfBirth)
                .Must(BeAtLeast18YearsOld).WithMessage("Person must be at least 18 years old.");

            RuleForEach(person => person.PhoneNumbers)
                .SetValidator(new PhoneNumberValidator());
        }

        private bool BeAtLeast18YearsOld(DateTime dob)
        {
            return dob <= DateTime.Today.AddYears(-18);
        }
    }
}
