using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.CustomFoods.Commands.Create;
using ThesisRestaurant.Application.CustomFoods.Commands.Update;
using ThesisRestaurant.Application.CustomFoods.Queries.GetById;
using ThesisRestaurant.Application.CustomFoods.Queries.GetMyCustomFoods;
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
                    customFood => Ok(_mapper.Map < CustomFoodResponse >(customFood)),
                    errors => Problem(errors)
                );
        }

        [HttpPut("{customFoodId:int}")]
        public async Task<IActionResult> UpdateCustomFood(CustomFoodRequest request, int userId, int customFoodId)
        {
            var command = _mapper.Map<UpdateCustomFoodCommand>((request, userId, customFoodId));
            var result = await _mediator.Send(command);

            return result.Match(
                    customFood => Ok(_mapper.Map<CustomFoodResponse>(customFood)),
                    errors => Problem(errors)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCustomFoods(int userId)
        {
            var query = new GetMyCustomFoodsQuery(userId);
            var result = await _mediator.Send(query);

            return result.Match(
                    customFoods => Ok(customFoods.Adapt<CustomFoodResponse[]>()),
                    errors => Problem(errors)
                ); 
        }

        [HttpGet("{customFoodId:int}")]
        public async Task<IActionResult> GetCustomFoodById(int userId, int customFoodId)
        {
            var query = new GetCustomFoodByIdQuery(userId, customFoodId);
            var result = await _mediator.Send(query);
            return result.Match(
                    customFood => Ok(_mapper.Map<CustomFoodResponse>(customFood)),
                    errors => Problem(errors)
                );
        }
    }
}
