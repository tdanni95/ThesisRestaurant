using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Domain.Orders.OrderCustomItems
{
    public class OrderCustomItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public CustomFood CustumFood { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
    }
}
