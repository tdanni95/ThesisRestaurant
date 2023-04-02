using ThesisRestaurant.Domain.Orders;

namespace ThesisRestaurant.Application.Cart.Common
{
    public record OrderResult(string User, List<Order> Order);
}
