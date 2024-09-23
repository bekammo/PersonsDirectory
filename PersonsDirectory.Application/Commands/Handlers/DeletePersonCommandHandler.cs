using MediatR;
using PersonsDirectory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(request.Id);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
            }

            _unitOfWork.Persons.Remove(person);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

}
