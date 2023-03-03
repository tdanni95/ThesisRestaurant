using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.IngredientTypes.Commands.Create;
using ThesisRestaurant.Application.IngredientTypes.Commands.Delete;
using ThesisRestaurant.Application.IngredientTypes.Commands.Update;
using ThesisRestaurant.Application.IngredientTypes.Common;
using ThesisRestaurant.Application.IngredientTypes.Queries.GetTypeById;
using ThesisRestaurant.Application.IngredientTypes.Queries.GetTypes;
using ThesisRestaurant.Application.IngredientTypes.Queries.GetWithIngredients;
using ThesisRestaurant.Contracts.IngredientTypes;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("ingredienttype")]
    [AllowAnonymous]
    public class IngredientTypesController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _meaditor;

        public IngredientTypesController(IMapper mapper, ISender meaditor)
        {
            _mapper = mapper;
            _meaditor = meaditor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientType(CreateIngredientTypeRequest request)
        {
            var command = _mapper.Map<CreateIngredientTypeCommand>(request);
            var createIngredientTypeResult = await _meaditor.Send(command);

            return createIngredientTypeResult.Match(
                    type => Ok(_mapper.Map<IngredientTypeResult>(type)),
                    error => Problem(error)
                    );
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredientTypes()
        {
            var query = new GetIngredientTypesQuery();
            var result = await _meaditor.Send(query);

            return result.Match(
                    ingredientTypes => Ok(ingredientTypes.Adapt<IngredientTypeResult[]>()),
                    error => Problem(error));
        }

        [HttpGet("ingredients")]
        public async Task<IActionResult> GetTypesWithIngredients()
        {
            var query = new GetWithIngredientsQuery();
            var result = await _meaditor.Send(query);

            return result.Match(
              ingredientTypes => Ok(ingredientTypes.Adapt<IngredientTypeIngredientsResult[]>()),
              error => Problem(error));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var query = new GetIngredientTypeByIdQuery(id);
            var result = await _meaditor.Send(query);
            return result.Match(
                    type => Ok(_mapper.Map<IngredientTypeResult>(type)),
                    error => Problem(error)
                    );
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIngredientType(int id)
        {
            var query = new DeleteIngredientTypeCommand(id);
            var result = await _meaditor.Send(query);
            return result.Match(
                    type => Ok(id),
                    error => Problem(error)
                    );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredientType(UpdateIngredientTypeRequest request)
        {
            var command = _mapper.Map<UpdateIngredientTypeCommand>(request);
            var result = await _meaditor.Send(command);

            return result.Match(
              type => Ok(_mapper.Map<IngredientTypeResult>(type)),
              error => Problem(error)
              );
        }
    }
}
