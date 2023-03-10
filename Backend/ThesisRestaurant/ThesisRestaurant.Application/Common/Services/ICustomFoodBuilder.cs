using ErrorOr;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Common.Services
{
    public interface ICustomFoodBuilder
    {
        Task<ErrorOr<CustomFood>> Create(string name, int foodTypeId, List<int> ingredientIds, int id = 0);
    }
}
