namespace ThesisRestaurant.Contracts.Food
{
    public record CreateFoodRequest(string Name, double BasePrice, int FoodTypeId, List<int> IngredientIds);
}
