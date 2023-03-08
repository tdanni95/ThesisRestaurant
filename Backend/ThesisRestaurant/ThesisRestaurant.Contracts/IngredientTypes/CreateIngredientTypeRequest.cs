namespace ThesisRestaurant.Contracts.IngredientTypes
{
    public record CreateIngredientTypeRequest(
            string Name, int MinOption, int MaxOption
        );
}
