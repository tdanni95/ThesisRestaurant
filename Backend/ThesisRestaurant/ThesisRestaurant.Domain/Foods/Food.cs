using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.Foods.FoodPictures;
using ThesisRestaurant.Domain.Foods.FoodPrices;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Domain.Foods
{
    public class Food
    {
        public int Id { get; private set; }
        [MaxLength(255)]
        public string Name { get; private set; }
        public FoodType Type { get; private set; }
        [DefaultValue(1)]
        public byte Visible { get; private set; } = 1;

        public double BasePrice { get; private set; }

        public List<FoodPicture> FoodPictures { get; set; } = new();
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<FoodPrice> FoodPrices { get; set; } = new();

        public List<OrderItem> Orders { get; set; } = new();
    }
}
