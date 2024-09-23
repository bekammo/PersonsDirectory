using MediatR;
using PersonsDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Queries
{
    public class GetRelatedPersonsReportQuery : IRequest<Dictionary<RelationshipType, int>>
    {
        public int PersonId { get; set; }
    }
}
