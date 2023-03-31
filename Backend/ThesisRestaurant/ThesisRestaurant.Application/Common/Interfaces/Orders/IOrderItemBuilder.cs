using ErrorOr;
using ThesisRestaurant.Application.Cart.Queries;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Application.Common.Interfaces.Orders
{
    public interface IOrderItemBuilder
    {
        //public Task<ErrorOr<OrderItem>> BuildOrderItem(int FoodId, int FoodSizeId, List<int> AdditionalIngredients);        
        public Task<ErrorOr<List<OrderItem>>> BuildOrderItem(List<OrderItemQuery> query);
    }
}
