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

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
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
            var user = new User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password };

            userRepository.Add(user);

            //Create user (generate unique id)
            //Create JWT token
            var token = jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
