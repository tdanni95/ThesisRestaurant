using ThesisRestaurant.Contracts.FoodType;
using ThesisRestaurant.Contracts.Ingredient;

namespace ThesisRestaurant.Contracts.Food
{
    public record FoodResponse(
             int Id, string Name, double BasePrice, double? DiscountPrice, 
             FoodTypeResponse FoodType, 
             List<IngredientResponse> Ingredients, 
             List<FoodPictureResponse> FoodPictures, 
             List<FoodPriceResponse> FoodPrices
        );

    public record FoodPictureResponse(
            int Id, string Src
        );

    public record FoodPriceResponse(
            int Id, double Price, DateTime From, DateTime To
        );
}
