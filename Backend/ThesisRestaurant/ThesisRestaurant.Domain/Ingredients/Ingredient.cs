using System.ComponentModel.DataAnnotations.Schema;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Domain.Ingredients
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        private Ingredient(string name, double price, int id = 0)
        {
            Name = name;
            Price = price;
            Id = id;
        }
        private Ingredient(string name, double price, IngredientType ingredientType, int id = 0)
        {
            Id = id;
            IngredientType = ingredientType;
            Name = name;
            Price = price;
        }

        public static Ingredient Create(string name, double price, IngredientType ingredientType, int id = 0)
        {
            return new(name, price, ingredientType, id);
        }

        public void Update(Ingredient ingredient)
        {
            this.Name = ingredient.Name;
            this.Price = ingredient.Price;
            this.IngredientType = ingredient.IngredientType;
        }

        public IngredientType IngredientType { get; set; }
        public List<Food> Foods { get; set; } = new();
        public List<CustomFood> CustomFoods { get; set; } = new();
    }
}
