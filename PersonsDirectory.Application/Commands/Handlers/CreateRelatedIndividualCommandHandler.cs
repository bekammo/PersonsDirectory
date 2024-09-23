using MediatR;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class CreateRelatedIndividualCommandHandler : IRequestHandler<CreateRelatedIndividualCommand, RelatedIndividual>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRelatedIndividualCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RelatedIndividual> Handle(CreateRelatedIndividualCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(request.PersonId);
            if (person == null)
            {
                return null;
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
