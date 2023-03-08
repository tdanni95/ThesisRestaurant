﻿using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Entities;
using ThesisRestaurant.Domain.Common.Errors;

namespace ThesisRestaurant.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHandler _passowrdHandler;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordHandler passwordHandler, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _passowrdHandler = passwordHandler;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            //user exists
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            bool isValidPassowrd = _passowrdHandler.VerifyPassword(query.Password, user.Password);
            //pw correct
            if (!isValidPassowrd)
            {
                return new[] { Errors.Authentication.InvalidCredentials };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            //create jwt and return
            return new AuthenticationResult(user, token);
        }
    }
}
