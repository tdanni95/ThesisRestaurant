using ErrorOr;

namespace ThesisRestaurant.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Foods
        {
            public static Error NotFound => Error.NotFound(code: "Food.NotFound", description: "Food not found.");
            public static Error InvalidDiscountDates => Error.Validation(code: "Food.FoodPriceError", "From date must be lower than to date.");
            public static Error InvalidDiscountValue => Error.Validation(code: "Food.FoodPriceError", "Discount price must be lower than regular price.");
        }
    }
}
