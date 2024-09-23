using FluentValidation.TestHelper;
using PersonsDirectory.Application.Validators;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Tests.Domain
{
    [TestFixture]
    public class PhoneNumberTests
    {
        [Test]
        public void PhoneNumber_ShouldCreateWithValidNumber()
        {

            var phoneNumber = new PhoneNumber
            {
                Number = "1234567890",
                Type = PhoneNumberType.Mobile
            };

            Assert.That(phoneNumber.Number, Is.EqualTo("1234567890"));
            Assert.That(phoneNumber.Type, Is.EqualTo(PhoneNumberType.Mobile));
        }

        [Test]
        public void PhoneNumber_ShouldHaveValidationError_WhenNumberIsInvalid()
        {
            var phoneNumber = new PhoneNumber
            {
                Number = "123", 
                Type = PhoneNumberType.Mobile
            };

            var validator = new PhoneNumberValidator();

            var result = validator.TestValidate(phoneNumber);

            result.ShouldHaveValidationErrorFor(phone => phone.Number)
                  .WithErrorMessage("Phone number must be between 4 and 50 characters.");
        }

    }
}
