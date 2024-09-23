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
    public class UpdatePersonCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public City City { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public string ImagePath { get; set; }
    }
}
