using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("order/{userId:int}")]
    [AllowAnonymous]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomFoodRepository _customFoodRepository;
        private readonly IUserRepository _userRepository;
        public OrderController(IOrderRepository orderRepository, ICustomFoodRepository customFoodRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _customFoodRepository = customFoodRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int userId = 8)
        {

            var customFoods = new List<CustomFood>();

            var myFood1 = await _customFoodRepository.GetById(5, userId);
            var myFood2 = await _customFoodRepository.GetById(4, userId);

            customFoods.Add(myFood1!);
            customFoods.Add(myFood1!);
            customFoods.Add(myFood2!);

            Dictionary<CustomFood, int> counts = new();

            foreach (var item in customFoods)
            {
                if (!counts.ContainsKey(item))
                {
                    counts[item] = 1;
                }
                else
                {
                    counts[item]++;
                }
            }

            List<OrderCustomItem> orders = new List<OrderCustomItem>();

            foreach (var item in counts)
            {
                OrderCustomItem oci = new()
                {
                    CustumFood = item.Key,
                    Quantity = (uint)item.Value,
                    Price = item.Key.Price * item.Value
                };

                orders.Add(oci);
            }

            Order o = new Order(0, DateTime.UtcNow, "3024, Lőrinci", new List<OrderItem>(), orders);

            var result = await _orderRepository.PlaceOrder(o, userId);

            return Ok(myFood1);
        }
    }
}
