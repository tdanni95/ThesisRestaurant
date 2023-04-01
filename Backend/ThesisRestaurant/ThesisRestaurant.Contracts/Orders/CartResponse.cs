using ThesisRestaurant.Contracts.CustomFood;
using ThesisRestaurant.Contracts.Food;
using ThesisRestaurant.Contracts.FoodSize;
using ThesisRestaurant.Contracts.Ingredient;

namespace ThesisRestaurant.Contracts.Orders
{
    public record CartResponse(List<OrderCustomItemResponse> CustomFoods, List<OrderItemResponse> Foods);
    public record OrderCustomItemResponse(int Id, string Name, double Price, string FoodType, int Quantity, List<OrderItemIngredientResponse> Ingredients);
    public record OrderItemIngredientResponse(string Name, string IngredientType);
    public record OrderItemResponse(
        int Id, string Name, string FoodType, string FoodSize, int FoodSizeId, double FoodSizeMultiplier, List<OrderItemIngredientResponse> Ingredients,
        List<OrderAdditionalIngredientResponse> AdditionalIngredients, 
        int Quantity, double Price);

    public record OrderAdditionalIngredientResponse(int Id, string Name, string IngredientType, double Price, int Quantity);
}
