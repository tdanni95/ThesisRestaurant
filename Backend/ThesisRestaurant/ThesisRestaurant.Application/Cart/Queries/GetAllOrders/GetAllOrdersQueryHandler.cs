using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Common;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.Cart.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, ErrorOr<List<OrderResult>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ErrorOr<List<OrderResult>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetAllOrders();

            if (result.IsError) return result.Errors;

            List<OrderResult> results = result.Value.ConvertAll(res => new OrderResult($"{res.FirstName} {res.LastName}", res.Orders));

            return results;
        }
    }
}
