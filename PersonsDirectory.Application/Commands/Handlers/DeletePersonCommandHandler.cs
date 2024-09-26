using FluentValidation;
using MediatR;
using PersonsDirectory.Application.Interfaces;

namespace PersonsDirectory.Application.Commands.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeletePersonCommand> _validator;

        public DeletePersonCommandHandler(IUnitOfWork unitOfWork, IValidator<DeletePersonCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

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
