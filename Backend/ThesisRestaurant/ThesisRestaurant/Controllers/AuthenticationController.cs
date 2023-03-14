using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Authentication.Commands.Register;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Authentication.Queries.Login;
using ThesisRestaurant.Contracts.Authentication;

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

            return authResult.Match(
                    authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
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
            //db-be megnézni melyik user-nek ez a tokenja
            //megnézni lejár-e
            return Ok();
        }
    }
}
