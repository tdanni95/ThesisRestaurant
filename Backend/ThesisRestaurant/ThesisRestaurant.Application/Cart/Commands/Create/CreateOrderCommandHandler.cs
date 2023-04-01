using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Orders;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Orders;

namespace ThesisRestaurant.Application.Cart.Commands.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Created>>
    {
        private readonly IOrderCustomItemBuilder _orderCustomItemBuilder;
        private readonly IOrderItemBuilder _orderItemBuilder;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderCustomItemBuilder orderCustomItemBuilder, IOrderItemBuilder orderItemBuilder, IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _orderCustomItemBuilder = orderCustomItemBuilder;
            _orderItemBuilder = orderItemBuilder;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ErrorOr<Created>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var customitems = await _orderCustomItemBuilder.BuildCustomItems(request.CustomFoodIds);
            if (customitems.IsError) return customitems.Errors;

            var items = await _orderItemBuilder.BuildOrderItem(request.OrderItems);
            if (items.IsError) return items.Errors;

            var user = await _userRepository.GetUserById(request.userId);
            if (user is null) return Errors.Users.NotFound;
            var address = user.UserAddresses.Where(ua => ua.Id == request.addressId).FirstOrDefault();
            if (address is null) return Errors.Users.AddressNotFound;

            Order o = Order.Create(address.GetFullAddress(), items.Value, customitems.Value);
            var result = await _orderRepository.PlaceOrder(o, request.userId);
            return result;
        }
    }
}
