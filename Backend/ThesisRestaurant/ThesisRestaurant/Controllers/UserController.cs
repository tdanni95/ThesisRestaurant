using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Users.Commands.Update;
using ThesisRestaurant.Contracts.User;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("user")]
    [AllowAnonymous]
    public class UserController : ApiController
    {
        /**
         * Cím hozzáadása
         * Cím módosítás
         * Cím törlés
         * User adat módosítás 
         * User level módosítás [admin only]
         * User PW módosítás
         * */
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public UserController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUser request)
        {
            var command = _mapper.Map<UpdateUserCommand>(request);
            var result = await _mediator.Send(command);
            return result.Match(
                    user => Ok(user),
                    errors => Problem(errors)
                );
        }
    }
}
