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
            config.NewConfig<UpdateFoodSizeRequest, UpdateFoodSizeCommand>()
                .Map(dest => dest.Name, src =>src.Name)
                .Map(dest => dest.FoodTypeId, src => src.FoodTypeId)
                .Map(dest => dest.Multiplier, src => src.Multiplier)
                .Map(dest => dest.Id, src => src.Id);
            config.NewConfig<FoodSize, FoodSizeResult>();
        }
    }
}
