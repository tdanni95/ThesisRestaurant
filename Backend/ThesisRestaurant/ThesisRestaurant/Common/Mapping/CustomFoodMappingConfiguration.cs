using Mapster;
using ThesisRestaurant.Application.CustomFoods.Commands.Create;
using ThesisRestaurant.Contracts.CustomFood;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class CustomFoodMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CustomFoodRequest request, int userId), CreateCustomFoodCommand>()
                .Map(dest => dest.UserId, src => src.userId)
                .Map(dest => dest, src => src.request);
        }
    }
}
