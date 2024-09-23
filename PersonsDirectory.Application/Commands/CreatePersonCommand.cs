using MediatR;
using PersonsDirectory.Domain.Entities;
using PersonsDirectory.Domain.Enums;
using PersonsDirectory.Domain.ValueObjects;

namespace PersonsDirectory.Application.Commands
{
    public class CreatePersonCommand : IRequest<Person>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public City City { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public string ImagePath { get; set; }
        public List<RelatedIndividual> RelatedIndividuals { get; set; }
    }

}
