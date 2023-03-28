using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Addresses.Commands.Create;
using ThesisRestaurant.Application.Addresses.Commands.Delete;
using ThesisRestaurant.Application.Addresses.Commands.Update;
using ThesisRestaurant.Application.Users.Commands.ChangeUserPassword;
using ThesisRestaurant.Application.Users.Commands.Update;
using ThesisRestaurant.Application.Users.Commands.UpdateUserLevel;
using ThesisRestaurant.Application.Users.Queries.GetAllUsers;
using ThesisRestaurant.Application.Users.Queries.GetUserById;
using ThesisRestaurant.Contracts.Authentication;
using ThesisRestaurant.Contracts.Ingredient;
using ThesisRestaurant.Contracts.User;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("user")]
    public class UserController : ApiController
    {
        /**
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);

            return result.Match(
                    users => Ok(users.Adapt<UserResponse[]>()),
                    errors => Problem(errors)
                );

        }

        [HttpPut("level")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserLevel(UpdateUserLevel request)
        {
            var command = _mapper.Map<UpdateUserLevelCommand>(request);
            var result = await _mediator.Send(command);
            return result.Match(
                    updated => NoContent(),
                    errors => Problem(errors)
                );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserData(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors)
                );
        }

        [HttpPost("address/{userId:int}")]
        public async Task<IActionResult> CreateAddress(RegisterAddressRequest request, int userId)
        {
            var command = _mapper.Map<CreateAddressCommand>((request, userId));
            var result = await _mediator.Send(command);

            return result.Match(
                    address => Ok(address),
                    errors => Problem(errors)
                );
        }

        [HttpPut("address/{userId:int}/{addressId:int}")]
        public async Task<IActionResult> UpdateAddress(RegisterAddressRequest request, int userId, int addressId)
        {
            var command = _mapper.Map<UpdateAddressCommand>((request, userId, addressId));
            var result = await _mediator.Send(command);

            return result.Match(
                    address => Ok(address),
                    errors => Problem(errors)
                );
        }

        [HttpDelete("address/{userId:int}/{addressId:int}")]
        public async Task<IActionResult> DeleteAddress(int userId, int addressId)
        {
            var command = new DeleteAddressCommand(userId, addressId);
            var result = await _mediator.Send(command);
            return result.Match(
                    address => NoContent(),
                    errors => Problem(errors)
                );
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



        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword(ChangeUserPassword request)
        {
            var command = _mapper.Map<ChangeUserPasswordCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                    updated => NoContent(),
                    errors => Problem(errors)
                );
        }
    }
}
