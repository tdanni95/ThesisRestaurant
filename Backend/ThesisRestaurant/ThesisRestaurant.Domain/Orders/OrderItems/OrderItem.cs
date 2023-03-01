using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Domain.Orders.OrderItems
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Food Food { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
    }
}
