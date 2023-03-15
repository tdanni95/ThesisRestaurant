using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Authentication.Commands.Register;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Authentication.Queries.Login;
using ThesisRestaurant.Application.Authentication.Queries.RefreshToken;
using ThesisRestaurant.Contracts.Authentication;
using ThesisRestaurant.Domain.Users.RefreshTokens;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("authentication")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _meaditor;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender meaditor, IMapper mapper)
        {
            _meaditor = meaditor;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await _meaditor.Send(command);
            if (!authResult.IsError)
            {
                SetRefreshToken(authResult.Value.RefreshToken);
            }
            return authResult.Match(
                    authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            ErrorOr<AuthenticationResult> authResult = await _meaditor.Send(query);
            if (!authResult.IsError)
            {
                SetRefreshToken(authResult.Value.RefreshToken);
            }
            return authResult.Match(
                    authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
                IsEssential = true
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

        /**
         * You should check if the server answers you with an 401 or 403, 
         * and if it does try to refresh the token calling the enpoint and try again, 
         * if that does not work, you simulate a log out deleting the auth jwt 
         * and redirecting the user to the login page.
         */
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            Console.WriteLine(refreshToken);
            var query = new RefreshTokenQuery(refreshToken);
            var result = await _meaditor.Send(query);
            if (!result.IsError)
            {
                SetRefreshToken(result.Value.RefreshToken);
            }
            return result.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors)
            );
        }
    }
}
