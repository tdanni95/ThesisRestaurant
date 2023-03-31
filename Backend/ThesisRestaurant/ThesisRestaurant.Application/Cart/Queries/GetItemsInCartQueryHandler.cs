using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Cart.Common;
using ThesisRestaurant.Application.Common.Interfaces.Orders;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Orders;
using static ThesisRestaurant.Domain.Common.Errors.Errors;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Application.Cart.Queries
{
    public class GetItemsInCartQueryHandler : IRequestHandler<GetItemsInCartQuery, ErrorOr<CartResponse>>
    {
        private readonly IOrderCustomItemBuilder _orderCustomItemBuilder;
        private readonly IOrderItemBuilder _orderItemBuilder;

        public GetItemsInCartQueryHandler(IOrderCustomItemBuilder orderCustomItemBuilder, IOrderItemBuilder orderItemBuilder)
        {
            _orderCustomItemBuilder = orderCustomItemBuilder;
            _orderItemBuilder = orderItemBuilder;
        }

        public async Task<ErrorOr<CartResponse>> Handle(GetItemsInCartQuery request, CancellationToken cancellationToken)
        {

            var customitems = await _orderCustomItemBuilder.BuildCustomItems(request.CustomFoodIds);
            if (customitems.IsError) return customitems.Errors;

            var items = await _orderItemBuilder.BuildOrderItem(request.OrderItems);
            if(items.IsError) return items.Errors;

            return new CartResponse(customitems.Value, items.Value);
        }
    }
}
