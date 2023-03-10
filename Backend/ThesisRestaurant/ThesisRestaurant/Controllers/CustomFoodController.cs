using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.CustomFoods.Commands.Create;
using ThesisRestaurant.Contracts.CustomFood;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("customfood/{userId:int}")]
    [AllowAnonymous]
    public class CustomFoodController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        public CustomFoodController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomFood(CustomFoodRequest request, int userId)
        {
            var command = _mapper.Map<CreateCustomFoodCommand>((request, userId));
            var result = await _mediator.Send(command);

            return result.Match(
                    customFood => Ok(customFood),
                    errors => Problem(errors)
                );
        }
    }
}
