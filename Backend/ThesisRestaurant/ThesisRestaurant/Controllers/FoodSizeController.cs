using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.FoodSizes.Commands.Create;
using ThesisRestaurant.Application.FoodSizes.Commands.Delete;
using ThesisRestaurant.Application.FoodSizes.Commands.Update;
using ThesisRestaurant.Application.FoodSizes.Common;
using ThesisRestaurant.Application.FoodSizes.Queries.GetAll;
using ThesisRestaurant.Application.FoodSizes.Queries.GetById;
using ThesisRestaurant.Contracts.FoodSize;
using ThesisRestaurant.Contracts.FoodType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("foodsize")]
    [AllowAnonymous]
    public class FoodSizeController : ApiController
    {
        private readonly ISender _meaditor;
        private readonly IMapper _mapper;

        public FoodSizeController(ISender meaditor, IMapper mapper)
        {
            _meaditor = meaditor;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodSize(CreateFoodSizeRequest request)
        {
            var command = _mapper.Map<CreateFoodSizeCommand>(request);
            var result = await _meaditor.Send(command);

            return result.Match(
                    foodSize => Ok(_mapper.Map<FoodSizeResult>(foodSize)),
                    error => Problem(error)
                    );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFoodSize(UpdateFoodSizeRequest request)
        {
            var command = _mapper.Map<UpdateFoodSizeCommand>(request);
            var result = await _meaditor.Send(command);
            return result.Match(
                    foodSize => Ok(_mapper.Map<FoodSizeResult>(foodSize)),
                    error => Problem(error)
                    );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFoodSize(int id)
        {
            var command = new DeleteFoodSizeCommand(id);
            var result = await _meaditor.Send(command);

            return result.Match(
                    deleted => Ok(id),
                    error => Problem(error)
                    );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllFoodSizeQuery();
            var result = await _meaditor.Send(query);

            return result.Match(
                    sizes => Ok(sizes.Adapt<FoodSizeResult[]>()),
                    errors => Problem(errors)
                );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetFoodSizeByIdQuery(id);
            var result = await _meaditor.Send(query);

            return result.Match(
                    foodSize => Ok(_mapper.Map<FoodSizeResult>(foodSize)),
                    error => Problem(error)
                    );
        }
    }
}
