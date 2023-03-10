using ThesisRestaurant.Contracts.FoodType;
using ThesisRestaurant.Contracts.Ingredient;

namespace ThesisRestaurant.Contracts.CustomFood
{
    public record CustomFoodResponse(
            int Id, string Name, double Price, FoodTypeResponse FoodType, List<IngredientResponse> Ingredients
        );
}
