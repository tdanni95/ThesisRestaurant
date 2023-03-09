using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Orders;

namespace ThesisRestaurant.Domain.CustomFoods
{
    public class CustomFood
    {
        public int Id { get; set; }
        public FoodType FoodType { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public double Price { get; set; }

        public double CalculatePrice()
        {
            return FoodType.Price + Ingredients.Sum(i => i.Price);
        }


        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}
