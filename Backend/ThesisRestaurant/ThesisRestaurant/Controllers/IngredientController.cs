using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Ingredients.Commands.Create;
using ThesisRestaurant.Application.Ingredients.Commands.Delete;
using ThesisRestaurant.Application.Ingredients.Commands.Update;
using ThesisRestaurant.Application.Ingredients.Common;
using ThesisRestaurant.Application.Ingredients.Queries.GetById;
using ThesisRestaurant.Application.Ingredients.Queries.GetByTypeId;
using ThesisRestaurant.Application.Ingredients.Queries.GetIngredients;
using ThesisRestaurant.Application.IngredientTypes.Commands.Delete;
using ThesisRestaurant.Contracts.Ingredient;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("ingredient")]
    public class IngredientController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _meaditor;
        public IngredientController(IMapper mapper, ISender meaditor)
        {
            _mapper = mapper;
            _meaditor = meaditor;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Create(CreateIngredientRequest request)
        {
            var command = _mapper.Map<CreateIngredientCommand>(request);
            var result = await _meaditor.Send(command);

            return result.Match(
                    ingredient => Ok(_mapper.Map<IngredientResponse>(ingredient)),
                    errors => Problem(errors));
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Update(UpdateIngredientRequest request)
        {
            var command = _mapper.Map<UpdateIngredientCommand>(request);
            var result = await _meaditor.Send(command);


            return result.Match(
                    ingredient => Ok(_mapper.Map<IngredientResponse>(ingredient)),
                    errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetIngredientsQuery();
            var result = await _meaditor.Send(query);

            return result.Match(
                    ingredients => Ok(ingredients.Adapt<IngredientResponse[]>()),
                    errors => Problem(errors)
                );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetIngredientByIdQuery(id);
            var result = await _meaditor.Send(query);

            return result.Match(
                    ingredient => Ok(_mapper.Map<IngredientResponse>(ingredient)),
                    errors => Problem(errors)
                    );
        }

        [HttpGet("type/{id:int}")]
        public async Task<IActionResult> GetByTypeId(int id)
        {
            var query = new GetIngredientByTypeIdQuery(id);
            var result = await _meaditor.Send(query);

            return result.Match(
                    ingredients => Ok(ingredients.Adapt<IngredientResponse[]>()),
                    errors => Problem(errors)
                );
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var query = new DeleteIngredientCommand(id);
            var result = await _meaditor.Send(query);
            return result.Match(
                    type => Ok(id),
                    error => Problem(error)
                    );
        }
    }
}
