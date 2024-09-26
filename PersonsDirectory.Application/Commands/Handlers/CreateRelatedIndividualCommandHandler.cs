using FluentValidation;
using MediatR;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class CreateRelatedIndividualCommandHandler : IRequestHandler<CreateRelatedIndividualCommand, RelatedIndividual>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateRelatedIndividualCommand> _validator;

        public CreateRelatedIndividualCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateRelatedIndividualCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<RelatedIndividual> Handle(CreateRelatedIndividualCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var person = await _unitOfWork.Persons.GetPersonWithDetailsAsync(request.PersonId);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {request.PersonId} not found.");
            }

            var relatedIndividual = new RelatedIndividual
            {
                RelatedPersonId = request.RelatedPersonId,
                RelationshipType = request.RelationshipType
            };

            person.RelatedIndividuals.Add(relatedIndividual); 
            _unitOfWork.Persons.Update(person);
            await _unitOfWork.CompleteAsync();

            return relatedIndividual;
        }
    }
}
