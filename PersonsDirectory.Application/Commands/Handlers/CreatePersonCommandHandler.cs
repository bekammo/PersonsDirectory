using FluentValidation;
using MediatR;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Person>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreatePersonCommand> _validator;

        public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IValidator<CreatePersonCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var person = new Person
            {
                Name = request.Name,
                Surname = request.Surname,
                Gender = request.Gender,
                PersonalNumber = request.PersonalNumber,
                DateOfBirth = request.DateOfBirth,
                City = request.City,
                PhoneNumbers = request.PhoneNumbers,
                ImagePath = request.ImagePath,
                RelatedIndividuals = request.RelatedIndividuals
            };

            await _unitOfWork.Persons.AddAsync(person);
            await _unitOfWork.CompleteAsync();

            return person;
        }
    }


}
