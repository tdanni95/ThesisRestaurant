using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.CustomFoods;
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

        public Food(int id, string name, byte visible, double basePrice)
        {
            Id = id;
            Name = name;
            Visible = visible;
            BasePrice = basePrice;
        }

        public Food(int id, string name, FoodType type, double basePrice, List<Ingredient> ingredients)
        {
            Id = id;
            Name = name;
            Type = type;
            BasePrice = basePrice;
            Ingredients = ingredients;
        }

        public static Food Create(string name, FoodType type, double basePrice, List<Ingredient> ingredients, int id = 0)
        {
            return new(id, name, type, basePrice, ingredients);
        }

        public void Update(Food food)
        {
            this.Name = food.Name;
            this.BasePrice = food.BasePrice;
            this.Type = food.Type;

            Ingredients.Clear();

            foreach (var ingredient in food.Ingredients)
            {
                Ingredients.Add(ingredient);
            }
        }

        public void AddDiscount(FoodPrice price)
        {
            this.FoodPrices.Add(price);
        }

        public void AddPicture(FoodPicture picture)
        {
            this.FoodPictures.Add(picture);
        }

        public FoodPrice? GetCurrentPrice()
        {
            var now = DateTime.Now;
            var price = this.FoodPrices.Where(fp => now >= fp.From && now <= fp.To).FirstOrDefault();
            return price;
        }

        public List<FoodPicture> FoodPictures { get; set; } = new();
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<FoodPrice> FoodPrices { get; set; } = new();

        public List<OrderItem> Orders { get; set; } = new();
    }
}
