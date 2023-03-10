using ErrorOr;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Common.Services
{
    public interface IFoodIngredientValidator
    {
        List<Error> ValidateIngredients(List<Ingredient> ingredients);
    }
}
