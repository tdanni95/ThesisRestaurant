using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.FoodTypes.Commands.Create;
using ThesisRestaurant.Application.FoodTypes.Commands.Delete;
using ThesisRestaurant.Application.FoodTypes.Commands.Update;
using ThesisRestaurant.Application.FoodTypes.Common;
using ThesisRestaurant.Application.FoodTypes.Queries.GetAll;
using ThesisRestaurant.Application.FoodTypes.Queries.GetById;
using ThesisRestaurant.Contracts.FoodType;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("foodtype")]
    [AllowAnonymous]
    public class FoodTypeController : ApiController
    {
        private readonly ISender _meaditor;
        private readonly IMapper _mapper;

        public FoodTypeController(ISender meaditor, IMapper mapper)
        {
            _meaditor = meaditor;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFoodTypeRequest request)
        {
            var command = _mapper.Map<CreateFoodTypeCommand>(request);
            var result = await _meaditor.Send(command);
            return result.Match(
                    foodType => Ok(_mapper.Map<FoodTypeResult>(foodType)),
                    errors => Problem(errors));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFoodTypeRequest request)
        {
            var command = _mapper.Map<UpdateFoodTypeCommand>(request);
            var result = await _meaditor.Send(command);

            return result.Match(
                    foodType => Ok(_mapper.Map<FoodTypeResult>(foodType)),
                    errors => Problem(errors));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteFoodTypeCommand(id);
            var result = await _meaditor.Send(command);

            return result.Match(
                    deleted => Ok(id),
                    errors => Problem(errors)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetFoodTypesQuery();
            var result = await _meaditor.Send(query);

            return result.Match(
                    foodTypes => Ok(foodTypes.Adapt<FoodTypeResult[]>()),
                    errors => Problem(errors));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetFoodTypeByIdQuery(id);
            var result = await _meaditor.Send(query);

            return result.Match(
                    foodType => Ok(_mapper.Map<FoodTypeResult>(foodType)),
                    errors => Problem(errors));
        }
    }
}
