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

            var customfoods = await _customFoodRepository.GetWhereidIn(ids);
            Dictionary<int, int> counts = BuildCustomFoodCounts(ids);

            List<OrderCustomItem> orderCustomItems = new List<OrderCustomItem>();

            foreach (var item in counts)
            {
                var customFood = customfoods.Where(cf => cf.Id == item.Key).First();
                OrderCustomItem oci = new()
                {
                    CustumFood = customFood,
                    Quantity = (uint)item.Value,
                    Price = customFood.Price * item.Value
                };

                orderCustomItems.Add(oci);
            }

            return orderCustomItems;
        }

        private Dictionary<int, int> BuildCustomFoodCounts(List<int> ids)
        {
            Dictionary<int, int> counts = new();

            foreach (var item in ids)
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
