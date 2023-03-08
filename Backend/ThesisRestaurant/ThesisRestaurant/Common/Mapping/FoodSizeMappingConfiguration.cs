using Mapster;
using ThesisRestaurant.Application.FoodSizes.Commands.Create;
using ThesisRestaurant.Application.FoodSizes.Commands.Update;
using ThesisRestaurant.Application.FoodSizes.Common;
using ThesisRestaurant.Contracts.FoodSize;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class FoodSizeMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateFoodSizeRequest, CreateFoodSizeCommand>();
            config.NewConfig<UpdateFoodSizeRequest, UpdateFoodSizeCommand>();
            config.NewConfig<FoodSize, FoodSizeResponse>();
        }
    }
}
