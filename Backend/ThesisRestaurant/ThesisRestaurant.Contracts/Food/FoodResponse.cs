using ThesisRestaurant.Contracts.FoodType;
using ThesisRestaurant.Contracts.Ingredient;

namespace ThesisRestaurant.Contracts.Food
{
    public record FoodResponse(
             int Id, string Name, double BasePrice, double? DiscountPrice, FoodTypeResponse FoodType, List<IngredientResponse> Ingredients
        );
}
