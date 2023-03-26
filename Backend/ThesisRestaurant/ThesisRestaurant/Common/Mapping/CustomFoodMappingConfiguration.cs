using Mapster;
using ThesisRestaurant.Application.CustomFoods.Commands.Create;
using ThesisRestaurant.Application.CustomFoods.Commands.Update;
using ThesisRestaurant.Contracts.CustomFood;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class CustomFoodMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CustomFoodRequest request, int userId), CreateCustomFoodCommand>()
                .Map(dest => dest.UserId, src => src.userId)
                .Map(dest => dest, src => src.request);

            config.NewConfig<(CustomFoodRequest request, int userId, int customFoodId), UpdateCustomFoodCommand>()
                .Map(dest => dest.UserId, src => src.userId)
                .Map(dest => dest.Id, src => src.customFoodId)
                .Map(dest => dest, src => src.request);

            config.NewConfig<CustomFood, CustomFoodResponse>()
                .Map(dest => dest.FoodType, src => src.FoodType)
                .Map(dest => dest, src => src);
        }
    }
}
