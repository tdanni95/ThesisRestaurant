using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;
using ThesisRestaurant.Domain.Orders.OrderAdditionalIngredients;

namespace ThesisRestaurant.Domain.Orders.OrderItems
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order? Order { get; set; }
        public Food Food { get; set; } = null!;
        public FoodSize FoodSize { get; set; } = null!;
        public uint Quantity { get; set; }
        public double Price { get; set; }

        public List<OrderAdditionalIngredient> AdditionalIngredients { get; set; } = new();
    }
}
