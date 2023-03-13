using ErrorOr;
using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Application.Common.Services
{
    public interface IFoodBuilder
    {
        Task<ErrorOr<Food>> Create(string name, double basePrice, int foodTypeId, List<int> ingredientIds, int id = 0);
    }
}
