using Mapster;
using ThesisRestaurant.Application.IngredientTypes.Commands.Create;
using ThesisRestaurant.Application.IngredientTypes.Commands.Update;
using ThesisRestaurant.Application.IngredientTypes.Common;
using ThesisRestaurant.Contracts.IngredientTypes;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class IngredientTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateIngredientTypeRequest, CreateIngredientTypeCommand>();
            config.NewConfig<IngredientType, IngredientTypeResult>();

            config.NewConfig<UpdateIngredientTypeRequest, UpdateIngredientTypeCommand>();
        }
    }
}
