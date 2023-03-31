using Mapster;
using ThesisRestaurant.Application.Cart.Queries;
using ThesisRestaurant.Contracts.Orders;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetItemsInCartQuery, OrderRequest>()
                .Map(dest => dest.CustomFoodIds, src => src.CustomFoodIds)
                .Map(dest => dest.OrderItems, src => src.OrderItems);
        }
    }
}
