using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _repository;
        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.Id);
            var result = await _repository.Update(user);
            if (result.IsError) return result.Errors;
            return user;
        }
    }
}
