using FluentValidation;
using PersonsDirectory.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Validators
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.")
                .Matches(@"^[\p{IsGeorgian}a-zA-Z]+$").WithMessage("Name must contain only Georgian or Latin letters.");

            RuleFor(person => person.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .Length(2, 50).WithMessage("Surname must be between 2 and 50 characters.")
                .Matches(@"^[\p{IsGeorgian}a-zA-Z]+$").WithMessage("Surname must contain only Georgian or Latin letters.");

            RuleFor(person => person.Gender)
                .IsInEnum().WithMessage("Gender must be either Male or Female.");

            RuleFor(person => person.PersonalNumber)
                .NotEmpty().WithMessage("Personal number is required.")
                .Matches(@"^\d{11}$").WithMessage("Personal number must be exactly 11 digits.");

            RuleFor(person => person.DateOfBirth)
                .Must(BeAtLeast18YearsOld).WithMessage("Person must be at least 18 years old.");

            RuleForEach(person => person.PhoneNumbers)
                .SetValidator(new PhoneNumberValidator());

            RuleForEach(person => person.RelatedIndividuals)
                .SetValidator(new RelatedIndividualValidator());
        }

        private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            return dateOfBirth <= DateTime.Today.AddYears(-18);
        }
    }

}
