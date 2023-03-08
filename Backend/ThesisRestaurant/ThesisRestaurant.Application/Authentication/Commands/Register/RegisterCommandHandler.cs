using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Entities;

namespace ThesisRestaurant.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHandler passwordHandler;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordHandler passwordHandler, IUserRepository userRepository)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.passwordHandler = passwordHandler;
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            //Validate the user doesnt exist
            if (userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Domain.Common.Errors.Errors.User.DuplicateEmail;
            }
            string hashedPassword = passwordHandler.HashPassword(command.Password);
            var user = new User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = hashedPassword };

            userRepository.Add(user);

            //Create user (generate unique id)
            //Create JWT token
            var token = jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
