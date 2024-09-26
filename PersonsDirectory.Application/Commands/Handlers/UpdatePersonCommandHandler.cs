using FluentValidation;
using MediatR;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdatePersonCommand> _validator;

        public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdatePersonCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var person = await _unitOfWork.Persons.GetPersonWithDetailsAsync(request.Id);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
            }

            person.Name = request.Name;
            person.Surname = request.Surname;
            person.Gender = request.Gender;
            person.PersonalNumber = request.PersonalNumber;
            person.DateOfBirth = request.DateOfBirth;
            person.City = request.City;
            person.ImagePath = request.ImagePath;

            if (person.PhoneNumbers == null)
            {
                person.PhoneNumbers = new List<PhoneNumber>();
            }

            person.PhoneNumbers.Clear();
            person.PhoneNumbers.AddRange(request.PhoneNumbers);

            _unitOfWork.Persons.Update(person);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }


}
