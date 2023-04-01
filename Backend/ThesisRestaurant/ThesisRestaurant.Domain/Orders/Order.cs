using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.CustomFoods;
using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Domain.Orders
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime Created { get; private set; } = DateTime.Now;

        public string Address { get; private set; }

        public Order(int id, DateTime created, string address)
        {
            Id = id;
            Created = created;
            Address = address;
        }

        public Order(int id, DateTime created, string address, List<OrderItem> foods, List<OrderCustomItem> customFoods)
        {
            Id = id;
            Created = created;
            Address = address;
            Foods = foods;
            CustomFoods = customFoods;
        }

        public static Order Create(string address, List<OrderItem> foods, List<OrderCustomItem> customFoods, int id = 0)
        {
            return new(id, DateTime.UtcNow, address, foods, customFoods);
        }

        public List<OrderItem> Foods { get; private set; } = new();
        public List<OrderCustomItem> CustomFoods { get; private set; } = new();
    }
}
