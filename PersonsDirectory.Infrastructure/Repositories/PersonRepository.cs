using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Application.Queries;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Infrastructure.Persistence;

namespace PersonsDirectory.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(PersonsDirectoryDbContext context) : base(context)
        {
        }

        public async Task<SearchPersonResult> SearchPersonAsync(SearchPersonQuery query)
        {
            var personsQuery = _context.Persons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                personsQuery = personsQuery.Where(p =>
                    p.Name.Contains(query.SearchTerm) ||
                    p.Surname.Contains(query.SearchTerm) ||
                    p.PersonalNumber.Contains(query.SearchTerm));
            }

            if (query.City.HasValue)
            {
                personsQuery = personsQuery.Where(p => p.City == query.City);
            }

            if (query.DateOfBirth.HasValue)
            {
                personsQuery = personsQuery.Where(p => p.DateOfBirth == query.DateOfBirth.Value);
            }

            var totalCount = await personsQuery.CountAsync();
            var results = await personsQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new SearchPersonResult
            {
                Results = results,
                TotalCount = totalCount
            };
        }

        public async Task<Dictionary<RelationshipType, int>> GetRelatedPersonsReportAsync(int personId)
        {
            return await _context.RelatedIndividuals
                .Where(ri => ri.RelatedPersonId == personId)
                .GroupBy(ri => ri.RelationshipType)
                .Select(group => new { RelationshipType = group.Key, Count = group.Count() })
                .ToDictionaryAsync(x => x.RelationshipType, x => x.Count);
        }

        public async Task<Person> GetPersonWithDetailsAsync(int id)
        {
            var person = await _context.Persons
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedIndividuals)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {id} not found.");
            }

            return person;
        }
    }
}
