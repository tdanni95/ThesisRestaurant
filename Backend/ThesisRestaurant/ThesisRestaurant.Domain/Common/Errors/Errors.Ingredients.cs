using ErrorOr;

namespace ThesisRestaurant.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Ingredients
        {
            public static Error NotFound => Error.NotFound(code: "Ingredient.NotFound", description: "Ingredient with given id does not exists");

            public static Error FoodCreatinCountError(string typeName, int min, int max) =>
                    Error.Validation(code: $"{String.Concat(typeName.Where(c => !Char.IsWhiteSpace(c)))}.CountError", 
                    description: $"{typeName} count must be between {min} and {max}");
            
        }
    }
}
