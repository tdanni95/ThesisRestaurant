namespace ThesisRestaurant.Contracts.IngredientTypes
{
    public record UpdateIngredientTypeRequest(int Id, int MinOption, int MaxOption, string Name);
}
