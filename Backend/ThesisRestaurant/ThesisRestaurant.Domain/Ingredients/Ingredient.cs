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
        public IngredientType IngredientType { get; set; }

        public List<Food> Foods { get; set; } = new();

        public List<CustomFood> CustomFoods { get; set; } = new();
    }
}
