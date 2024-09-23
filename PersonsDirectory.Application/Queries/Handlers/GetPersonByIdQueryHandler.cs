using MediatR;
using PersonsDirectory.Application.Interfaces;

namespace PersonsDirectory.Application.Queries.Handlers
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, GetPersonByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPersonByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPersonByIdResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(request.Id);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
            }

            return new GetPersonByIdResponse
            {
                Person = person,
                RelatedIndividuals = person.RelatedIndividuals,
                ImagePath = person.ImagePath
            };
        }
    }

}
