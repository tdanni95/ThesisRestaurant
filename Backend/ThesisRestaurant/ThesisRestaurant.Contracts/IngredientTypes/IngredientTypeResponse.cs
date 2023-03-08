using ThesisRestaurant.Contracts.Ingredient;

namespace ThesisRestaurant.Contracts.IngredientTypes
{
    public record IngredientTypeResponse(int Id, int MinOption, int MaxOption, string Name);

    public record IngredientTypeIngredientsResponse(int Id, string Name, List<IngredientResponse> Ingredients);
}
