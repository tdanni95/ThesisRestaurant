using ThesisRestaurant.Application.FoodSizes.Common;

namespace ThesisRestaurant.Application.FoodTypes.Common
{
    public record FoodTypeResult(int Id, string Name);
    public record FoodTypeSizesResult(int Id, string Name, List<FoodSizeResult> Ingredients);
}
