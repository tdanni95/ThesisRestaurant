namespace ThesisRestaurant.Contracts.Orders
{
    public record OrderRequest(List<int> CustomFoodIds, List<OrderItemRequest> OrderItems);

    public record OrderItemRequest(int FoodId, int FoodSizeId, List<int> AdditionalIngredients);
}
