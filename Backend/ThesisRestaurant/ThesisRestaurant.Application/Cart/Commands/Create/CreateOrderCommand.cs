using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Queries;

namespace ThesisRestaurant.Application.Cart.Commands.Create
{
    public record CreateOrderCommand(int userId, int addressId, List<int> CustomFoodIds, List<OrderItemQuery> OrderItems) : IRequest<ErrorOr<Created>>;
}
