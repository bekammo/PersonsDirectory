using MediatR;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Person>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                Name = request.Name,
                Surname = request.Surname,
                Gender = request.Gender,
                PersonalNumber = request.PersonalNumber,
                DateOfBirth = request.DateOfBirth,
                City = request.City,
                PhoneNumbers = request.PhoneNumbers,
                ImagePath = request.ImagePath,
                RelatedIndividuals = request.RelatedIndividuals
            };

            await _unitOfWork.Persons.AddAsync(person);
            await _unitOfWork.CompleteAsync();

            return person;
        }
    }
}
