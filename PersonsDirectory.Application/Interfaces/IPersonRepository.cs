using PersonsDirectory.Application.Queries;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<SearchPersonResult> SearchPersonAsync(SearchPersonQuery query);
        Task<Dictionary<RelationshipType, int>> GetRelatedPersonsReportAsync(int personId);
        Task<Person> GetPersonWithDetailsAsync(int id);
    }
}
