using ErrorOr;
using ThesisRestaurant.Application.Cart.Queries;
using ThesisRestaurant.Application.Common.Interfaces.Orders;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Orders.OrderAdditionalIngredients;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Infrastructure.Services
{
    public class OrderItemBuilder : IOrderItemBuilder
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodSizeRepository _foodSizeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public OrderItemBuilder(IFoodRepository foodRepository, IFoodSizeRepository foodSizeRepository, IIngredientRepository ingredientRepository)
        {
            _foodRepository = foodRepository;
            _foodSizeRepository = foodSizeRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<ErrorOr<List<OrderItem>>> BuildOrderItem(List<OrderItemQuery> query)
        {
            var counts = OrderItemCounts(query);
            var orderItems = new List<OrderItem>();

            foreach (var queryItem in query)
            {
                int FoodId = queryItem.FoodId;
                int FoodSizeId = queryItem.FoodSizeId;
                List<int> AdditionalIngredients = queryItem.AdditionalIngredients;
                

                var orderItem = new OrderItem();

                var food = await _foodRepository.GetFoodById(FoodId);
                if (food is null) return Errors.Foods.NotFound;
                var foodSize = await _foodSizeRepository.GetById(FoodSizeId);
                if (foodSize.IsError) return foodSize.Errors;

                var ingredients = await _ingredientRepository.GetWhereIdIn(AdditionalIngredients);
                if (ingredients.IsError) return ingredients.Errors;

                var additionalIngredients = BuildAdditionalIngredientIdsWithCount(AdditionalIngredients);

                var currPrice = food.GetCurrentPrice();
                double finalPrice = currPrice is not null ? currPrice.Price : food.BasePrice;
                finalPrice *= foodSize.Value.Multiplier;

                var oais = new List<OrderAdditionalIngredient>();

                foreach (var item in additionalIngredients)
                {
                    OrderAdditionalIngredient oai = new();
                    var ingredient = ingredients.Value.Where(i => i.Id == item.Key).First();
                    int count = item.Value;
                    finalPrice += ingredient.Price * foodSize.Value.Multiplier;

                    oai.Ingredient = ingredient;
                    oai.Quantity = count;

                    oais.Add(oai);
                }

                orderItem.Food = food;
                orderItem.FoodSize = foodSize.Value;
                orderItem.Quantity = 1;
                orderItem.AdditionalIngredients = oais;

                orderItems.Add(orderItem);
            }
            return orderItems;
        }

        private Dictionary<string, int> OrderItemCounts(List<OrderItemQuery> query)
        {
            Dictionary<string, int> counts = new();
            foreach (var item in query)
            {
                //ahol van + feltét az különálló
                if (item.AdditionalIngredients.Count != 0) continue;
                string key = $"{item.FoodId}-{item.FoodSizeId}";
                if (!counts.ContainsKey(key))
                {
                    counts[key] = 1;
                }

                counts[key]++;
            }

            return counts;
        }

        public Dictionary<int, int> BuildAdditionalIngredientIdsWithCount(List<int> AdditionalIngredients)
        {
            Dictionary<int, int> ingredientCount = new();

            foreach (var ingredientId in AdditionalIngredients)
            {
                if (!ingredientCount.ContainsKey(ingredientId))
                {
                    ingredientCount[ingredientId] = 0;
                }
                ingredientCount[ingredientId]++;
            }

            return ingredientCount;
        }
    }
}
