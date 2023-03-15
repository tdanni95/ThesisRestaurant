using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderAdditionalIngredients;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;
using ThesisRestaurant.Infrastructure.Persistence.Repositories;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("order/{userId:int}")]
    [AllowAnonymous]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomFoodRepository _customFoodRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IFoodSizeRepository _foodSizeRepository;
        public OrderController(IOrderRepository orderRepository, ICustomFoodRepository customFoodRepository,
            IUserRepository userRepository, IFoodRepository foodRepository, IIngredientRepository ingredientRepository, IFoodSizeRepository foodSizeRepository)
        {
            _orderRepository = orderRepository;
            _customFoodRepository = customFoodRepository;
            _userRepository = userRepository;
            _foodRepository = foodRepository;
            _ingredientRepository = ingredientRepository;
            _foodSizeRepository = foodSizeRepository;
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

            List<OrderItem> orderItems = new();

            var food1 = await _foodRepository.GetFoodById(1);
            var food2 = await _foodRepository.GetFoodById(1);
            var food3 = await _foodRepository.GetFoodById(2);

            var ingredient = await _ingredientRepository.GetById(8);
            var ingredient2 = await _ingredientRepository.GetById(9);

            var foodSize = await _foodSizeRepository.GetById(8);

            OrderAdditionalIngredient oai = new();
            oai.Ingredient = ingredient.Value;
            oai.Quantity = 2;

            OrderAdditionalIngredient oai2 = new();
            oai.Ingredient = ingredient2.Value;
            oai2.Quantity = 1;

            List<Food> foods = new();
            foods.Add(food1);
            foods.Add(food2);
            foods.Add(food3);

            int i = 0;

            foreach (var food in foods)
            {
                List<OrderAdditionalIngredient> oail = new();
                var currPrice = food.GetCurrentPrice();
                double finalPrice = currPrice is not null ? currPrice.Price : food.BasePrice;
                finalPrice *= foodSize.Value.Multiplier;
                if (i == 0)
                {
                    finalPrice += foodSize.Value.Multiplier * ingredient.Value.Price * 2;
                    var additionaling = new OrderAdditionalIngredient()
                    {
                        Ingredient = ingredient.Value,
                        Quantity = 2,
                    };
                    oail.Add(additionaling);
                }
                else if (i == 2)
                {
                    finalPrice += foodSize.Value.Multiplier * ingredient2.Value.Price * 1;
                    var additionaling = new OrderAdditionalIngredient()
                    {
                        Ingredient = ingredient2.Value,
                        Quantity = 1,
                    };
                    oail.Add(additionaling);
                }

                OrderItem oi = new()
                {
                    Food = food,
                    Quantity = 1,
                    Price = Math.Round(finalPrice),
                    FoodSize = foodSize.Value,
                    AdditionalIngredients = oail
                };

                orderItems.Add(oi);
                i++;
            }

            Order o = new Order(0, DateTime.UtcNow, "3024, Lőrinci", orderItems, orders);


            var result = await _orderRepository.PlaceOrder(o, userId);

            return Ok(myFood1);
        }
    }
}
