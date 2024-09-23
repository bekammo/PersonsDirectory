using MediatR;
using PersonsDirectory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(request.Id);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
            }

            person.Name = request.Name;
            person.Surname = request.Surname;
            person.Gender = request.Gender;
            person.PersonalNumber = request.PersonalNumber;
            person.DateOfBirth = request.DateOfBirth;
            person.City = request.City;
            person.ImagePath = request.ImagePath;

            person.PhoneNumbers.Clear();
            person.PhoneNumbers.AddRange(request.PhoneNumbers);

            _unitOfWork.Persons.Update(person);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

}
