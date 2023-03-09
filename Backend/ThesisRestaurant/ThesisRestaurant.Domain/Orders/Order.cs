using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.CustomFoods;
using System.ComponentModel.DataAnnotations;

namespace ThesisRestaurant.Domain.Orders
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime Created { get; private set; } = DateTime.Now;

        public string Address { get; private set; }

        public List<Food> Foods { get; private set; } = new();
        public List<CustomFood> CustomFoods { get; private set; } = new();
    }
}
