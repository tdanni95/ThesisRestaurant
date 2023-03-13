using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Foods.Commands.Create;
using ThesisRestaurant.Application.Foods.Queries.GetAll;
using ThesisRestaurant.Application.Foods.Queries.GetById;
using ThesisRestaurant.Contracts.Food;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("food")]
    [AllowAnonymous]
    public class FoodController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _meaditor;

        public FoodController(IMapper mapper, ISender meaditor)
        {
            _mapper = mapper;
            _meaditor = meaditor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFoods()
        {
            var query = new GetAllFoodQuery();
            var result = await _meaditor.Send(query);
            return result.Match(
                    foods => Ok(foods.Adapt<FoodResponse[]>()),
                    errors => Problem(errors)
                );
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFoodById(int id)
        {
            var query = new GetFoodByIdQuery(id);
            var result = await _meaditor.Send(query);
            return result.Match(
               food => Ok(_mapper.Map<FoodResponse>(food)),
               errors => Problem(errors)
           );
        }

        [HttpPost]
        public async Task<IActionResult> CreateFood(CreateFoodRequest request)
        {
            var command = _mapper.Map<CreateFoodCommand>(request);
            var result = await _meaditor.Send(command);
            return result.Match(
                    food => Ok(_mapper.Map<FoodResponse>(food)),
                    errors => Problem(errors)
                );
        }
    }
}
