using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Application.Foods.Common
{
    public record FoodResult(double DiscountPrice, Food Food);
}
