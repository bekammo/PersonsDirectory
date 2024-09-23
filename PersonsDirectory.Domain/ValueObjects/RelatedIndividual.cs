using PersonsDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Domain.ValueObjects
{
    public class RelatedIndividual
    {
        public int Id { get; set; }
        public RelationshipType RelationshipType { get; set; } 
        public int RelatedPersonId { get; set; }
        public int PersonId { get; set; }
    }

}
