using FluentValidation;
using PersonsDirectory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Validators
{
    public class PhoneNumberValidator : AbstractValidator<PhoneNumber>
    {
        public PhoneNumberValidator()
        {
            RuleFor(phone => phone.Type)
                .IsInEnum().WithMessage("Phone number type must be Mobile, Office, or Home.");

            RuleFor(phone => phone.Number)
                .NotEmpty().WithMessage("Phone number is required.")
                .Length(4, 50).WithMessage("Phone number must be between 4 and 50 characters.");
        }
    }
}
