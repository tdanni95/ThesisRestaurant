using Mapster;
using ThesisRestaurant.Application.Ingredients.Commands.Create;
using ThesisRestaurant.Application.Ingredients.Commands.Update;
using ThesisRestaurant.Application.IngredientTypes.Common;
using ThesisRestaurant.Contracts.Ingredient;
using ThesisRestaurant.Contracts.IngredientTypes;
using ThesisRestaurant.Domain.Ingredients;

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


            config.NewConfig<Ingredient, IngredientResponse>()
                .Map(dest => dest.IngredientType, src => src.IngredientType)
                .Map(dest => dest, src => src);
        }
    }
}
