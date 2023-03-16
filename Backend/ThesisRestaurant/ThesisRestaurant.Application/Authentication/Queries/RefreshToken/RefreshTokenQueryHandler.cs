using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;

namespace ThesisRestaurant.Application.Authentication.Queries.RefreshToken
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RefreshTokenQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            //get user by refresh token; not null because the validator already checked it
            var user = await _userRepository.GetuserByRefreshToken(request.refreshToken!);
            if(user is null)
            {
                return Errors.Users.NotFound;
            }
            var now = DateTime.UtcNow;
            //check if token date is valid
            if(user.RefreshToken!.Expires < now)
            {
                return Errors.Users.BadToken;
            }
            //generate new refresh token
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            //simulate login
            await _userRepository.Login(user, refreshToken);
            //create jwt
            var jwt = _jwtTokenGenerator.GenerateToken(user);
            //return authresult
            return new AuthenticationResult(user, jwt, refreshToken);
        }
    }
}
