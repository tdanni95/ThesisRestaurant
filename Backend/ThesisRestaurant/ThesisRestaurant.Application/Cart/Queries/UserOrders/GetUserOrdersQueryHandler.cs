using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Common;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Cart.Queries.UserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, ErrorOr<OrderResult>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetUserOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ErrorOr<OrderResult>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetUserOrders(request.Id);
            if (result.IsError) return result.Errors;
            return new OrderResult($"{result.Value.FirstName} {result.Value.LastName}", result.Value.Orders);
        }
    }
}
