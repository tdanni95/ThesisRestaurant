using ErrorOr;

namespace ThesisRestaurant.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class CustomFoods
        {
            public static Error NotFound => Error.NotFound(code: "CustomFood.NotFound", description: "CustomFood not found");
            public static Error Auth => Error.Validation(code: "CustomFood.Unathorized", description: "You can't acces this custom food");
        }
    }
}
