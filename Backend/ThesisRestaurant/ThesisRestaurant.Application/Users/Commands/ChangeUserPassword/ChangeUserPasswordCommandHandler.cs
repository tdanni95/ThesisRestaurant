using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;

namespace ThesisRestaurant.Application.Users.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHandler _passwordHandler;

        public ChangeUserPasswordCommandHandler(IUserRepository repository, IPasswordHandler passwordHandler)
        {
            _repository = repository;
            _passwordHandler = passwordHandler;
        }

        public async Task<ErrorOr<Updated>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserById(request.Id);
            //user exists
            if (user is null)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            bool isValidPassowrd = _passwordHandler.VerifyPassword(request.OldPassword, user.Password);
            
            if (!isValidPassowrd)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            var newPassword = _passwordHandler.HashPassword(request.NewPassword);
            var result = await _repository.ChangePassword(newPassword, request.Id);
            return result;
        }
    }
}
