using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Common;
using ThesisRestaurant.Application.Common.Interfaces.Orders;

namespace ThesisRestaurant.Application.Cart.Queries
{
    public class GetItemsInCartQueryHandler : IRequestHandler<GetItemsInCartQuery, ErrorOr<CartResult>>
    {
        private readonly IOrderCustomItemBuilder _orderCustomItemBuilder;
        private readonly IOrderItemBuilder _orderItemBuilder;

        public GetItemsInCartQueryHandler(IOrderCustomItemBuilder orderCustomItemBuilder, IOrderItemBuilder orderItemBuilder)
        {
            _orderCustomItemBuilder = orderCustomItemBuilder;
            _orderItemBuilder = orderItemBuilder;
        }

        public async Task<ErrorOr<CartResult>> Handle(GetItemsInCartQuery request, CancellationToken cancellationToken)
        {

            var customitems = await _orderCustomItemBuilder.BuildCustomItems(request.CustomFoodIds);
            if (customitems.IsError) return customitems.Errors;

            var items = await _orderItemBuilder.BuildOrderItem(request.OrderItems);
            if(items.IsError) return items.Errors;

            return new CartResult(customitems.Value, items.Value);
        }
    }
}
