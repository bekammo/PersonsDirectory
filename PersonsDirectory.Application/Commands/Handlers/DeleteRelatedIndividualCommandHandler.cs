using FluentValidation;
using MediatR;
using PersonsDirectory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class DeleteRelatedIndividualCommandHandler : IRequestHandler<DeleteRelatedIndividualCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeleteRelatedIndividualCommand> _validator;

        public DeleteRelatedIndividualCommandHandler(IUnitOfWork unitOfWork, IValidator<DeleteRelatedIndividualCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteRelatedIndividualCommand request, CancellationToken cancellationToken)
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

            var relatedIndividual = person.RelatedIndividuals
                .FirstOrDefault(r => r.RelatedPersonId == request.RelatedPersonId);

            if (relatedIndividual == null)
            {
                throw new KeyNotFoundException($"Related individual with ID {request.RelatedPersonId} not found for person with ID {request.PersonId}.");
            }

            person.RelatedIndividuals.Remove(relatedIndividual);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

}
