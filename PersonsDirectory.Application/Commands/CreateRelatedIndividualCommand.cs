using MediatR;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Commands
{
    public class CreateRelatedIndividualCommand : IRequest<RelatedIndividual>
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }

}
