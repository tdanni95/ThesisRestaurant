using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Queries;

namespace ThesisRestaurant.Application.Cart.Commands.Create
{
    public record CreateOrderCommand(int UserId, int AddressId, List<int> CustomFoodIds, List<OrderItemQuery> OrderItems) : IRequest<ErrorOr<Created>>;
}
