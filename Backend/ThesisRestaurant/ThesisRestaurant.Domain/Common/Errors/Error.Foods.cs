using ErrorOr;

namespace ThesisRestaurant.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Foods
        {
            public static Error NotFound => Error.NotFound(code: "Food.NotFound", description: "Food not found");
        }
    }
}
