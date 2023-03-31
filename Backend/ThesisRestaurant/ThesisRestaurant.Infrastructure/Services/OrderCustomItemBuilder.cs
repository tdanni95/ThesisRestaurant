using ErrorOr;
using ThesisRestaurant.Application.Common.Interfaces.Orders;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;

namespace ThesisRestaurant.Infrastructure.Services
{
    public class OrderCustomItemBuilder : IOrderCustomItemBuilder
    {
        private readonly ICustomFoodRepository _customFoodRepository;

        public OrderCustomItemBuilder(ICustomFoodRepository customFoodRepository)
        {
            _customFoodRepository = customFoodRepository;
        }

        public async Task<ErrorOr<List<OrderCustomItem>>> BuildCustomItems(List<int> ids)
        {
            Dictionary<CustomFood, int> counts = await BuildCustomFoodCounts(ids);

            List<OrderCustomItem> orderCustomItems = new List<OrderCustomItem>();

            foreach (var item in counts)
            {
                OrderCustomItem oci = new()
                {
                    CustumFood = item.Key,
                    Quantity = (uint)item.Value,
                    Price = item.Key.Price * item.Value
                };

                orderCustomItems.Add(oci);
            }

            return orderCustomItems;
        }

        private async Task<Dictionary<CustomFood, int>> BuildCustomFoodCounts(List<int> ids)
        {
            Dictionary<CustomFood, int> counts = new();

            var customfoods = await _customFoodRepository.GetWhereidIn(ids);
            foreach (var item in customfoods)
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

            return counts;
        }
    }
}
