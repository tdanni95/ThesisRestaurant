using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Common;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Cart.Queries.UserOrders
{
    public record GetUserOrdersQuery(int Id) : IRequest<ErrorOr<OrderResult>>;
}
