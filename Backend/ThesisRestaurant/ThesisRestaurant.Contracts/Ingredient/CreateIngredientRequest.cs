using ThesisRestaurant.Contracts.IngredientTypes;

namespace ThesisRestaurant.Contracts.Ingredient
{
    public record CreateIngredientRequest(
             string Name, double Price, int IngredientTypeId
        );

    

}
