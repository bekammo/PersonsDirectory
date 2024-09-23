using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Commands
{
    public class DeleteRelatedIndividualCommand : IRequest<Unit>
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
    }
}
