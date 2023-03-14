using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Domain.Orders.OrderAdditionalIngredients
{
    public class OrderAdditionalIngredient
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}
