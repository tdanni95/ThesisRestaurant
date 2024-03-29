﻿using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Users;
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
            var user = await _userRepository.GetUserByEmail(query.Email);
            //user exists
            if (user is null)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            
            bool isValidPassowrd = _passowrdHandler.VerifyPassword(query.Password, user.Password);
            //pw correct
            if (!isValidPassowrd)
            {
                return new[] { Errors.Authentication.InvalidCredentials };
            }

            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            await _userRepository.Login(user, refreshToken);

            var jwt = _jwtTokenGenerator.GenerateToken(user);
            //create jwt and return
            return new AuthenticationResult(user, jwt, refreshToken);
        }
    }
}
