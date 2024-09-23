using MediatR;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Queries.Handlers
{
    public class GetRelatedPersonsReportQueryHandler : IRequestHandler<GetRelatedPersonsReportQuery, Dictionary<RelationshipType, int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRelatedPersonsReportQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<RelationshipType, int>> Handle(GetRelatedPersonsReportQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Persons.GetRelatedPersonsReportAsync(request.PersonId);
        }
    }

}
