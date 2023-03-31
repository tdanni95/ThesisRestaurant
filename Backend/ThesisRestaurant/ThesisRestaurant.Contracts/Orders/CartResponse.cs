using ThesisRestaurant.Contracts.CustomFood;
using ThesisRestaurant.Contracts.Food;
using ThesisRestaurant.Contracts.FoodSize;
using ThesisRestaurant.Contracts.Ingredient;

namespace ThesisRestaurant.Contracts.Orders
{
    public record CartResponse(List<OrderCustomItemResponse> CustomFoods, List<OrderItemResponse> Foods);
    public record OrderCustomItemResponse(List<CustomFoodResponse> CustomFoods, int Quantity);
    public record OrderItemResponse(FoodResponse Food, FoodSizeResponse FoodSize, List<IngredientResponse> AdditionalIngredients, int Quantity, double Price);
}
