using Mapster;
using ThesisRestaurant.Application.Foods.Commands.Create;
using ThesisRestaurant.Application.Foods.Common;
using ThesisRestaurant.Contracts.Food;
using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class FoodMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateFoodRequest, CreateFoodCommand>();

            config.NewConfig<FoodResult, FoodResponse>()
                .Map(dest => dest.DiscountPrice, src => src.DiscountPrice)
                .Map(dest => dest.FoodType, src => src.Food.Type)
                .Map(dest => dest, src => src.Food);
        }
    }
}
