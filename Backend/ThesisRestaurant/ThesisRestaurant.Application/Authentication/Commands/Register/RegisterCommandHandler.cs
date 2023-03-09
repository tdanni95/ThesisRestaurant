using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.UserAddresses;

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
            //Validate the user doesnt exist
            var isUser = await userRepository.GetUserByEmail(command.Email);
            if (isUser is not null)
            {
                return Errors.User.DuplicateEmail;
            }
            string hashedPassword = passwordHandler.HashPassword(command.Password);
            var user = User.Create(
                    command.FirstName,
                    command.LastName,
                    command.Email,
                    hashedPassword,
                    command.PhoneNumber,
                    string.Empty,
                    1,
                    command.Addresses.ConvertAll(address => UserAddress.Create(address.ZipCode, address.City, address.Street, address.HouseNumber))
                );
            var token = jwtTokenGenerator.GenerateToken(user);

            await userRepository.Add(user);

            //Create user (generate unique id)
            //Create JWT token
            return new AuthenticationResult(user, token);
        }
    }
}
