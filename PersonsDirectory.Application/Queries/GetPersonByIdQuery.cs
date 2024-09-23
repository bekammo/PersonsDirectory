using MediatR;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Queries
{
    public class GetPersonByIdQuery : IRequest<GetPersonByIdResponse>
    {
        public int Id { get; set; }
    }

    public class GetPersonByIdResponse
    {
        public Person Person { get; set; }
        public List<RelatedIndividual> RelatedIndividuals { get; set; }
        public string ImagePath { get; set; }
    }
}
