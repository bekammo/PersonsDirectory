using MediatR;
using PersonsDirectory.Application.Interfaces;

namespace PersonsDirectory.Application.Queries.Handlers
{
    public class SearchPersonQueryHandler : IRequestHandler<SearchPersonQuery, SearchPersonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchPersonQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SearchPersonResult> Handle(SearchPersonQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Persons.SearchPersonAsync(request);
        }
    }
}
