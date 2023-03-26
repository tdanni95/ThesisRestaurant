using Mapster;
using ThesisRestaurant.Application.Foods.Commands.Create;
using ThesisRestaurant.Application.Foods.Commands.Discount;
using ThesisRestaurant.Application.Foods.Commands.Update;
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

            config.NewConfig<(CreateFoodRequest request, int id), UpdateFoodCommand>()
                .Map(dest => dest.Id, src => src.id)
                .Map(dest => dest, src => src.request);

            config.NewConfig<FoodResult, FoodResponse>()
                .Map(dest => dest.DiscountPrice, src => src.DiscountPrice)
                .Map(dest => dest.FoodType, src => src.Food.Type)
                .Map(dest => dest.FoodPrices, src => src.Food.FoodPrices)
                .Map(dest => dest.FoodPictures, src => src.Food.FoodPictures)
                .Map(dest => dest, src => src.Food);

            config.NewConfig<(DiscountRequest request, int foodId), AddDiscountCommand>()
                .Map(dest => dest.FoodId, src => src.foodId)
                .Map(dest => dest, src => src.request);
        }
    }
}
