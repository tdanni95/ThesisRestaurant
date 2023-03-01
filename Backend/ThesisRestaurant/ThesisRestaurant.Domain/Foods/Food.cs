using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.Foods.FoodPictures;
using ThesisRestaurant.Domain.Foods.FoodPrices;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Orders;

namespace ThesisRestaurant.Domain.Foods
{
    public class Food
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public FoodType Type { get; set; }
        [DefaultValue(1)]
        public byte Visible { get; set; } = 1;

        public List<FoodPicture> FoodPictures { get; set; } = new();
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<FoodPrice> FoodPrices { get; set; } = new();

        public List<Order> Orders { get; set; } = new();
    }
}
