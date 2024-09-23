using MediatR;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Queries
{
    public class SearchPersonQuery : IRequest<SearchPersonResult>
    {
        public string SearchTerm { get; set; }
        public City? City { get; set; } 
        public DateTime? DateOfBirth { get; set; } 
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class SearchPersonResult
    {
        public IEnumerable<Person> Results { get; set; }
        public int TotalCount { get; set; }
    }
}
