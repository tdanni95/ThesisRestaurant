namespace ThesisRestaurant.Contracts.CustomFood
{
    public record CustomFoodRequest(
            string Name, int FoodTypeId, List<int> IngredientIds
        );
}
