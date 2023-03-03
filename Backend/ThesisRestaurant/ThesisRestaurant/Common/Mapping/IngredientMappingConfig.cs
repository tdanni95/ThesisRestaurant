using Mapster;
using ThesisRestaurant.Application.Ingredients.Commands.Create;
using ThesisRestaurant.Application.Ingredients.Commands.Update;
using ThesisRestaurant.Application.Ingredients.Common;
using ThesisRestaurant.Application.IngredientTypes.Common;
using ThesisRestaurant.Contracts.Ingredient;
using ThesisRestaurant.Contracts.IngredientTypes;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class IngredientMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateIngredientRequest, CreateIngredientCommand>()
                .Map(dest => dest, src => src);

            config.NewConfig<IngredientTypeRequest, IngredientTypeCommand>();

            config.NewConfig<UpdateIngredientRequest, UpdateIngredientCommand>();


            config.NewConfig<Ingredient, IngredientResult>()
                .Map(dest => dest.IngredientTypeResult, src => src.IngredientType)
                .Map(dest => dest, src => src);
        }
    }
}
