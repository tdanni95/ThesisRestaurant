using ThesisRestaurant.Contracts.IngredientTypes;

namespace ThesisRestaurant.Contracts.Ingredient
{
    public record IngredientResponse(int Id, string Name, double Price, IngredientTypeResponse IngredientType);
}
