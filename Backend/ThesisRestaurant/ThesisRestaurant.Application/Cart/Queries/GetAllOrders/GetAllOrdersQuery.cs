using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Common;

namespace ThesisRestaurant.Application.Cart.Queries.GetAllOrders
{
    public record GetAllOrdersQuery() : IRequest<ErrorOr<List<OrderResult>>>;
}
