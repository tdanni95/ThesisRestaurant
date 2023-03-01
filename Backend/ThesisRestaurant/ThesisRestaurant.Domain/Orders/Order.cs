using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public List<Food> Foods { get; set; } = new();
        public List<CustomFood> CustomFoods { get; set; } = new();
    }
}
