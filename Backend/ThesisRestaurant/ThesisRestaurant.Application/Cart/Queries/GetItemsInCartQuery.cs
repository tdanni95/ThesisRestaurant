using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Common;
using ThesisRestaurant.Domain.Orders;

namespace ThesisRestaurant.Application.Cart.Queries
{
    public record GetItemsInCartQuery(List<int> CustomFoodIds, List<OrderItemQuery> OrderItems) : IRequest<ErrorOr<CartResult>>;

    public record OrderItemQuery(int FoodId, int FoodSizeId, List<int> AdditionalIngredients);
}
