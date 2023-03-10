using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;

namespace ThesisRestaurant.Domain.CustomFoods
{
    public class CustomFood
    {
        public int Id { get; private set; }
        public FoodType FoodType { get; private set; }
        [MaxLength(255)]
        public string Name { get; private set; }
        public double Price { get; private set; }

        public void CalculatePrice()
        {
            this.Price = FoodType.Price + Ingredients.Sum(i => i.Price);
        }

        public static double CalculatePrice(FoodType foodType, List<Ingredient> ingredients)
        {
            return foodType.Price + ingredients.Sum(i => i.Price);
        }
        private CustomFood(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        private CustomFood(int id, string name, double price, List<Ingredient> ingredients, FoodType foodType)
        {
            Id = id;
            FoodType = foodType;
            Name = name;
            Price = price;
            Ingredients = ingredients;
        }

        public static CustomFood Create(string name, double price, List<Ingredient> ingredients, FoodType foodType, int id = 0)
        {
            return new(id, name, price, ingredients, foodType);
        }

        public void Update(CustomFood customFood)
        {
            this.Name = customFood.Name;
            this.Price = customFood.Price;

            Ingredients.Clear();

            foreach (var ingredient in customFood.Ingredients)
            {
                Ingredients.Add(ingredient);
            }

            this.FoodType = customFood.FoodType;
        }

        public List<Ingredient> Ingredients { get; set; } = new();
        public List<OrderCustomItem> Orders { get; private set; } = new();
    }
}
